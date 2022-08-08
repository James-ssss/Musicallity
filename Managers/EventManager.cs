using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using Discord.Commands;
using Victoria;
using Microsoft.Extensions.DependencyInjection;


namespace Musicallity.Managers
{
    public static class EventManager
    {
        private static LavaNode _lavaNode = ServiceManager.Provider.GetRequiredService<LavaNode>();
        private static DiscordSocketClient _client = Managers.ServiceManager.GetService<DiscordSocketClient>();
        private static CommandService _commandService = Managers.ServiceManager.GetService<CommandService>();

        public static Task LoadCommands()
        {

            _client.Log += message =>
            {
                Console.WriteLine($"[{DateTime.Now}]\t({message.Source})\t{message.Message}");
                return Task.CompletedTask;
            };
            _commandService.Log += message =>
            {
                Console.WriteLine($"[{DateTime.Now}]\t({message.Source})\t{message.Message}");
                return Task.CompletedTask;
            };
            _client.Ready += OnReady;
            _client.MessageReceived += OnMessageReceived;

            return Task.CompletedTask;
        }

        private static async Task OnMessageReceived(SocketMessage arg)
        {
            var message = arg as SocketUserMessage;
            var context = new SocketCommandContext(_client, message);

            if (message.Author.IsBot || message.Channel is IDMChannel) return;

            var argPos = 0;
            if (!(message.HasStringPrefix(ConfigManager.Config.Prefix, ref argPos) || message.HasMentionPrefix(_client.CurrentUser, ref argPos))) return;

            var result = await _commandService.ExecuteAsync(context, argPos, ServiceManager.Provider);
            if (!result.IsSuccess)
            {
                if (result.Error == CommandError.UnknownCommand) return;
            }
        }

        private static async Task OnReady()
        {

            try
            {
                await _lavaNode.ConnectAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }



            Console.WriteLine($"[{DateTime.Now}]\t(READY)\tBOT is ready");
            await _client.SetStatusAsync(UserStatus.Online);
            await _client.SetGameAsync($"Prefix: {ConfigManager.Config.Prefix}");
        }
    }
}
