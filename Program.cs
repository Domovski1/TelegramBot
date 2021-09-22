using System;
using System.Collections.Generic;
using System.IO;
using Telegram.Bot;
using Telegram.Bot.Args;
using TelegramBot.Classes;

namespace TelegramBot
{
    class Program
    {
        /// <summary>
        /// API Token для бота
        /// </summary>
        private static string token { get; set; } = "1777573276:AAE7ePm8ZTn4rSVjq9HkNNI27eNf3eFPyCo";

        /// <summary>
        /// Ссылка на документ, в котором хранятся все пользователи
        /// </summary>
        private static string Path = Environment.CurrentDirectory + @"/" + "TelegramBot.txt";
        private static string log = Environment.CurrentDirectory + @"/" + "old TelegramBot.txt";

        /// <summary>
        /// Клиент, для работы с командами
        /// </summary>
        private static TelegramBotClient client;

        static void Main(string[] args)
        {
            Console.WriteLine("Bot is running....");
            client = new TelegramBotClient(token);
            client.StartReceiving();
            client.OnMessage += OnMessageHandler;
            Console.ReadLine();
            client.StopReceiving();
            
        }


        /// <summary>
        /// Обработчик сообщений
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static async void OnMessageHandler(object sender, MessageEventArgs e)   
        {
            var MessageFromUser = e.Message;

            // Проверка пользователя, является ли он новым, если да, то его в список юзеров, нет - логи
            if (!IsUserRegistred(MessageFromUser.From.Id))
            {
                Console.WriteLine($"[INFO] Новый пользователь. Имя {MessageFromUser.From}: \nlink: {MessageFromUser.Text}");
                StreamWriter writer = new StreamWriter(Path, true);
                writer.WriteLine($"{MessageFromUser.From}: ({DateTime.Now})");
                writer.Close();
            } else
            {
                Console.WriteLine($"{MessageFromUser.From}: {MessageFromUser.Text}");
                StreamWriter writer = new StreamWriter(log, true);
                writer.WriteLine($"{ MessageFromUser.From}: {MessageFromUser.Text} ({DateTime.Now}");
                writer.Close();
            }



            try
            {
                // Информация о боте
                if (MessageFromUser.Text.ToLower().Contains("\\start") || MessageFromUser.Text.ToLower().Contains("\\info"))
                {
                    await client.SendTextMessageAsync(MessageFromUser.Chat.Id, "Я являюсь загрузчиком видео с Youtube. \nВам нужно лишь отправить мне ссылку на желаемое видео и я скачаю его для вас");
                }
                // Основной код
                else
                {
                    try
                    {
                        VideoFromYoutubeToAudio.SaveVideoToDisk(MessageFromUser.Text);
                        Console.WriteLine($"[INFO] Video downloaded \nlink: {MessageFromUser.Text}");

                        var vid = await client.SendVideoAsync(
                                chatId: MessageFromUser.Chat.Id,
                                video: File.OpenRead(VideoFromYoutubeToAudio.FileName),
                                thumb: "https://sun9-57.userapi.com/impg/4b7GLehH0kM2PkUP53sxOvf7d09pe49DedXAhQ/nOJWRVm7z_Q.jpg?size=1953x1255&quality=96&sign=14aee579f9e7b078f24e9301c69b75df&type=album",
                                replyToMessageId: MessageFromUser.MessageId,
                                supportsStreaming: true);
                    }
                    catch (Exception ex)
                    {
                        await client.SendTextMessageAsync(MessageFromUser.Chat.Id, "Упс, что-то пошло не так.. \nВозможно вы ввели неверную команду \nМожно вызвать информацию через команду \\info");
                        Console.WriteLine(ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("[INFO] " + ex);
            }
            
        }
        


        /// <summary>
        /// Проверка наличия Id в базе
        /// </summary>
        /// <param name="UserId">ID пользователя</param>
        /// <returns></returns>
        private static bool IsUserRegistred(int UserId)
        {
            List<string> Clietns = new List<string>();

            var doc = File.ReadAllLines(Path);

            foreach (var item in doc)
            {
                if (item.Contains($"{UserId.ToString()}"))
                {
                    return true;
                }
            }

            return false;
        }


        
    }
}

// Можно доработать (конверт то мпз, гет фром инст)