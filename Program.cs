namespace Musicallity
{
    public class Program
    {
        //private static Thread musicThread = null;
    

        
        //private DiscordSocketClient client;
        public static void Main()
            => new Bot().MainAsync().GetAwaiter().GetResult();
            //StartNewClient();
        

        /*private static async void StartNewClient()
        {
            var client = new DiscordSocketClient();
            client.MessageReceived += CommandHandler;
            client.Log += PrintLog;
            await client.LoginAsync(TokenType.Bot, Settings.token);
            await client.StartAsync();
            Console.ReadKey();
        }

       

        private static Task PrintLog(LogMessage logMessage)
        {
            Console.WriteLine(logMessage.ToString());
            return Task.CompletedTask;
        }

        private static async Task CommandHandler(SocketMessage msg)
        {
            if (msg.ToString().Length == 0)
            {
                return;
            }
            if (!msg.Author.IsBot && msg.ToString()[0] == '-')
            {
                var rnd = new Random();
                string[] photos;
                var ask = msg.Content.Split("\n");
                foreach (var p in ask)
                    switch (p)
                    {
                        case "-help":
                            string strOut = "```";
                            foreach (var elem in File.ReadAllLines("..//..//..//help.txt"))
                                strOut += elem + "\n";
                            strOut += "```";
                            await msg.Channel.SendMessageAsync(strOut);
                            break;
                        case "-gachi":
                        
                            photos = Directory.GetFiles("..//..//..//pictures/gachi");
                            await msg.Channel.SendFileAsync(photos[rnd.Next(0, photos.Length)]);
                            break;
                        case "-deko":

                            photos = Directory.GetFiles("..//..//..//pictures/deko");
                            await msg.Channel.SendFileAsync(photos[rnd.Next(0, photos.Length)]);
                            break;
                        case "-gg":
                            photos = Directory.GetFiles("..//..//..//pictures/gg");
                            await msg.Channel.SendFileAsync(photos[rnd.Next(0, photos.Length)]);
                            break;
                        case "-csgo":
                            photos = Directory.GetFiles("..//..//..//pictures/csgo");
                            await msg.Channel.SendFileAsync(photos[rnd.Next(0, photos.Length)]);
                            break;
                        case "-dora":
                            photos = Directory.GetFiles("..//..//..//pictures/dora");
                            await msg.Channel.SendFileAsync(photos[rnd.Next(0, photos.Length)]);
                            break;
                        case "-roll":
                            await msg.Channel.SendMessageAsync(rnd.Next(0, 100).ToString());
                            break;
                        /////////////////////////////////////////////////////////////////////
                        case "-play":

                            if (musicThread == null)
                            {
                                musicThread = new Thread(() => new Commands(msg).PlayMusicInVoiceChat(msg));
                                musicThread.Start();
                                
                            }
                            else
                                await msg.Channel.SendMessageAsync("Уже запущен, брат.");

                            break;
                        case "-stop":
                            if (musicThread != null)
                            {
                                musicThread.Join();
                                musicThread = null;
                                new Commands(msg).DeleteBotFromVoiceChat();

                            }

                            else
                                await msg.Channel.SendMessageAsync("Молчу, брат.");
                            break;

                        default:
                            await msg.Channel.SendMessageAsync("Не знаю такой команды, брат.");
                            break;
                    }
            }
                
            await Task.CompletedTask;*/
        //}
    
    
    }
}


