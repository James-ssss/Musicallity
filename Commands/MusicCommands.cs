using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace Musicallity.Commands
{
    [Name("Music")]
    public class MusicCommands : ModuleBase<SocketCommandContext>
    {
        [Command("join")]
        [Summary("чтобы бот заходил в войсчат")]
        public async Task JoinCommand()
            => await Context.Channel.SendMessageAsync(await Managers.AudioManager.JoinAsync(Context.Guild, Context.User as IVoiceState, Context.Channel as ITextChannel));

        [Command("play")]
        [Summary("запускает видос с ютуба")]
        public async Task PlayCommand([Remainder] string search)
            => await Context.Channel.SendMessageAsync(await Managers.AudioManager.PlayAsync(Context.User as SocketGuildUser, Context.Guild, search));

        [Command("leave")]
        [Summary("чтобы бот выходил из войсчата")]
        public async Task LeaveCommand()
            => await Context.Channel.SendMessageAsync(await Managers.AudioManager.LeaveAsync(Context.Guild));
        [Command("pause")]
        [Alias("resume")]
        [Summary("play/pause music")]
        public async Task PauseCommand()
            => await Context.Channel.SendMessageAsync(await Managers.AudioManager.TogglePauseAsync(Context.Guild));

        [Command("skip")]
        [Summary("переход к следующему треку")]
        public async Task SkipCommand()
            => await Context.Channel.SendMessageAsync(await Managers.AudioManager.SkipTrackAsync(Context.Guild));

    }
}
