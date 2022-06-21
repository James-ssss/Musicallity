using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using Discord.Audio;
//using Sodium;
//using Xabe.FFmpeg;
//using Concentus;

namespace MusicallityBot
{
    public  class Commands
    {
        private IVoiceChannel channel;

        public Commands(SocketMessage msg)
        {
            channel = ((IGuildUser)msg.Author).VoiceChannel;
        }
        public async  void PlayMusicInVoiceChat(SocketMessage msg)
        {
            channel = channel ?? ((IGuildUser)msg.Author).VoiceChannel;
            if (channel == null) { await msg.Channel.SendMessageAsync("User must be in a voice channel, or a voice channel must be passed as an argument."); return; }         
            IAudioClient audioClient = null;
            audioClient = await channel.ConnectAsync(true);

        }
        public  async void DeleteBotFromVoiceChat()
        {
            await channel.DisconnectAsync();
            channel = null;
        }
    }
}
