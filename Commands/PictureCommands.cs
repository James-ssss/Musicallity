using Discord.Commands;


namespace Musicallity.Commands
{
    [Name("Picture")]
    public class PictureCommands : ModuleBase<SocketCommandContext>
    {
        [Command("dora")]
        [Summary("скидывает фотку доры")]
        public async Task DoraCommand()
        {
            var rnd = new Random();
            string[] photos;
            photos = Directory.GetFiles("..//..//..//pictures/dora");
            await Context.Channel.SendFileAsync(photos[rnd.Next(0, photos.Length)]);
        }

        [Command("deko")]
        [Summary("цитатка deko")]
        public async Task DekoCommand()
        {
            var rnd = new Random();
            string[] photos;
            photos = Directory.GetFiles("..//..//..//pictures/deko");
            await Context.Channel.SendFileAsync(photos[rnd.Next(0, photos.Length)]);
        }

        [Command("cs")]
        [Summary("киберспортивный мемчик")]
        public async Task CsgoCommand()
        {
            var rnd = new Random();
            string[] photos;
            photos = Directory.GetFiles("..//..//..//pictures/csgo");
            await Context.Channel.SendFileAsync(photos[rnd.Next(0, photos.Length)]);
        }


    }
}
