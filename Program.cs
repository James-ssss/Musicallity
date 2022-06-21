
using System;
using System.Threading.Tasks;
using System.IO;
using Discord;
using Discord.WebSocket;
using Discord.Audio;
//using Sodium;
//using Xabe.FFmpeg;
//using Concentus;


namespace MusicallityBot
{


    class Program
    {
        private static Thread musicThread = null;
    

        
        private DiscordSocketClient client;
        public static async Task Main()
        {
            StartNewClient();
        }

        private static async void StartNewClient()
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
                switch (msg.Content)
                {
                    case "-play":

                        if (musicThread == null)
                        {
                            musicThread = new Thread(() => new Commands(msg).PlayMusicInVoiceChat(msg));
                            musicThread.Start();
                        }
                        else
                            await msg.Channel.SendMessageAsync("Уже запущен, брат.");
                        //audioClient.CreatePCMStream(AudioApplication.Mixed);
                        break;
                    case "-help":
                        string strOut = "```";
                        foreach (var elem in File.ReadAllLines("..//..//..//help.txt"))
                            strOut += elem + "\n";
                        strOut += "```";
                        await msg.Channel.SendMessageAsync(strOut);
                        break;
                    case "-gachi":
                        
                        photos = Directory.GetFiles("..//..//..//gachi");
                        await msg.Channel.SendFileAsync(photos[rnd.Next(0, photos.Length)]);
                        break;
                    case "-kenny9":
                        
                        photos = Directory.GetFiles("..//..//..//kenny9");
                        await msg.Channel.SendFileAsync(photos[rnd.Next(0, photos.Length)]);
                        break;
                    case "-gg":
                        photos = Directory.GetFiles("..//..//..//gg");
                        await msg.Channel.SendFileAsync(photos[rnd.Next(0, photos.Length)]);
                        break;
                    case "-dora":
                        photos = Directory.GetFiles("..//..//..//dora");
                        await msg.Channel.SendFileAsync(photos[rnd.Next(0, photos.Length)]);
                        break;
                    case "-hui" or "-chlen" or "-dick":
                        photos = Directory.GetFiles("..//..//..//dick");
                        await msg.Channel.SendFileAsync(photos[rnd.Next(0, photos.Length)]);
                        break;
                    case "-roll":
                        await msg.Channel.SendMessageAsync(rnd.Next(0, 100).ToString());
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
                
            await Task.CompletedTask;
        }
    
    
    }
}


