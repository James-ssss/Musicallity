using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using Victoria;
using Victoria.Enums;
using Victoria.EventArgs;
using Victoria.Responses.Search;



namespace Musicallity.Managers
{
    public static class AudioManager
    {
        private static readonly LavaNode _lavaNode = ServiceManager.Provider.GetRequiredService<LavaNode>();
        
        public static async Task<string> JoinAsync(IGuild guild, IVoiceState voiceState, ITextChannel textChannel)
        {
            if (_lavaNode.HasPlayer(guild)) return "Уже playing нахуй";
            if (voiceState.VoiceChannel is null) return "Сначала в войсчат зайди, дурачок))";
            try
            {
                await _lavaNode.JoinAsync(voiceState.VoiceChannel, textChannel);
                return $"Сегодня я за диджея))";
            }
            catch (Exception ex)
            {
                    
                return $"ERROR\n{ex.Message}";
            }
        }
        public static async Task<string> PlayAsync(SocketGuildUser user, IGuild guild, string query)
        {
            if (user.VoiceChannel is null) return "Сначала в войсчат зайди, дурачок))";

            if (!_lavaNode.HasPlayer(guild)) return "Я не подключился к войсчату";

            try
            {
                var player = _lavaNode.GetPlayer(guild);
                LavaTrack track;
                var search = Uri.IsWellFormedUriString(query, UriKind.Absolute) 
                    ? await _lavaNode.SearchAsync(SearchType.SoundCloud,query) 
                    : await _lavaNode.SearchYouTubeAsync(query);
                if (search.Status == SearchStatus.NoMatches) return "Братанчик, спроси че попроще";
                track = search.Tracks.FirstOrDefault();
                if (player.Track != null && player.PlayerState is PlayerState.Playing || player.PlayerState is PlayerState.Paused)
                {
                    player.Queue.Enqueue(track);
                    Console.WriteLine($"[{DateTime.Now}]\t(AUDIO)\tTrack was added to queue");
                    return $"[{track.Title}] был добавлен в очередь";
                }
                await player.PlayAsync(track);
                Console.WriteLine($"Now playing /{track.Author}/ - [{track.Title}]");
                return $"Сейчас играет: [{track.Title}]";
            }

            catch (Exception ex)
            {

                return $"ERROR: {ex.Message}";
            }

        }
        public static async Task<string> LeaveAsync(IGuild guild)
        {
            try
            {
                var player = _lavaNode.GetPlayer(guild);
                if (player.PlayerState is PlayerState.Playing) await player.StopAsync();
                await _lavaNode.LeaveAsync(player.VoiceChannel);

                Console.WriteLine($"[{DateTime.Now}]\t(AUDIO)\tBot has left a voicechat ");
                return "Я ливаю";
            }
            catch (InvalidOperationException ex)
            {

                return $"ERROR: {ex.Message}";
            }
        }
        public static async Task<string> SkipTrackAsync(IGuild guild)
        {
            try
            {
                var player = _lavaNode.GetPlayer(guild);
                if (player == null) return "Дядь, у тебя ничего не играет";
                if (player.Queue.Count < 1)
                {
                    return "У тебя в очереди нет треков, зачем ты скипаешь последний трек????";
                }
                else
                {
                    try
                    {
                        var currentTrack = player.Track;
                        await player.SkipAsync();
                        Console.WriteLine($"[{DateTime.Now}]\t(AUDIO)\tBot skipped: {currentTrack.Title}");
                        //_lavaNode.GetPlayer(guild).  $"**Skipped** {currentTrack.Title}";
                        return $"Сейчас играет: [{player.Track.Title}]";

                    }
                    catch (Exception ex)
                    {

                        return $"ERROR: {ex.Message}";
                    }
                }
            }
            catch (Exception ex)
            {

                return $"ERROR: {ex.Message}";
            }
        }
        public static  async Task<string> TogglePauseAsync(IGuild guild)
        {
            try
            {
                var player = _lavaNode.GetPlayer(guild);
                if (!(player.PlayerState is PlayerState.Playing) && player.Track == null) return "Сейчас ничего не играет";
                if (!(player.PlayerState is PlayerState.Playing))
                {
                    await player.ResumeAsync();
                    return $"**RESUMED** {player.Track.Title}";
                }
                await player.PauseAsync();
                return $"**PAUSED** {player.Track.Title}";
            }
            catch (InvalidOperationException ex)
            {

                return ex.Message;
            }
        }
        /// <summary>
        ///     Whether the next track should be played or not.
        /// </summary>
        /// <param name="trackEndReason">Track end reason given by Lavalink.</param>
        public static bool ShouldPlayNext(this TrackEndReason trackEndReason)
        {
            return trackEndReason == TrackEndReason.Finished || trackEndReason == TrackEndReason.LoadFailed;
        }
        public static async Task TrackEnded(TrackEndedEventArgs args)
        {
            if (!args.Reason.ShouldPlayNext()) return;

            if (!args.Player.Queue.TryDequeue(out var queueable )) return;
            
            if (!(queueable is LavaTrack track))
            {
                await args.Player.TextChannel.SendMessageAsync("Следующая песенка играть не будет.|.");
                return;
            }
            await args.Player.PlayAsync(track);
            await args.Player.TextChannel.SendMessageAsync($"Сейчас играет: /{track.Author}/ - [{track.Title}]");
        }
    }
}
