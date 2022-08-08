using Discord.Commands;


namespace Musicallity.Commands
{
    public class OtherCommands : ModuleBase<SocketCommandContext>
    {
        
                            
        [Command("roll")]
        [Summary("число от 0 до 100")]
        public async Task RollCommand()
        {
            var rnd = new Random();
            await Context.Channel.SendMessageAsync(rnd.Next(0, 100).ToString());
        }
    }
}
