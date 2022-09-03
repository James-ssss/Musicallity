using Discord.Commands;



namespace Musicallity.Commands
{
    public class OtherCommands : ModuleBase<SocketCommandContext>
    {
        [Command("help")]
        [Summary("список команд")]
        public async Task HelpCommand()
        {
            string strOut = "```";
            foreach (var elem in File.ReadAllLines("..//..//..//help.txt"))
                strOut += elem + "\n";
            strOut += "```";
            await Context.Channel.SendMessageAsync(strOut);
        }
        [Command("roll")]
        [Summary("число от 0 до 100")]
        public async Task RollCommand()
        {
            var rnd = new Random();
            await Context.Channel.SendMessageAsync(rnd.Next(0, 100).ToString());
        }


    }
}
