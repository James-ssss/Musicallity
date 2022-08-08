using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Discord;
using Discord.Commands;

namespace Musicallity.Managers
{
    public static class CommandManager
    {
        private static CommandService _commandService = Managers.ServiceManager.GetService<CommandService>();
        
        public static async Task LoadCommandAsync()
        {
            await _commandService.AddModulesAsync(Assembly.GetEntryAssembly(), Managers.ServiceManager.Provider);
            foreach (var command in _commandService.Commands)
            {
                Console.WriteLine($"Команда {command.Name} была загружена");
            }
        }
    }
}
