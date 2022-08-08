using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using Victoria;

namespace Musicallity
{
    public class Bot
    {
        private DiscordSocketClient _client;
        private CommandService _commandService;

        public Bot()
        {
            _client = new DiscordSocketClient(new DiscordSocketConfig() 
            { 
                LogLevel = LogSeverity.Debug
            });
            _commandService = new CommandService(new CommandServiceConfig()
            {
                LogLevel = LogSeverity.Debug,
                CaseSensitiveCommands = true,
                DefaultRunMode = RunMode.Async,
                IgnoreExtraArgs = true

            });
               
            var collection = new ServiceCollection();
            collection.AddSingleton(_client);
            collection.AddSingleton(_commandService);
            collection.AddLavaNode(x =>
            {
                x.SelfDeaf = false;
            });
            Managers.ServiceManager.SetProvider(collection);

        }
        public async Task MainAsync()
        {
            if (string.IsNullOrWhiteSpace(Managers.ConfigManager.Config.Token)) return;

            await Managers.CommandManager.LoadCommandAsync();
            await Managers.EventManager.LoadCommands();
            await _client.LoginAsync(TokenType.Bot, Managers.ConfigManager.Config.Token);
            await _client.StartAsync();

            await Task.Delay(-1);
        }
    }
}
