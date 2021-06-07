using System;
using System.Collections.Generic;
using System.IO;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot
{
    class Program
    {
        private static string token { get; set; } = "1777573276:AAE7ePm8ZTn4rSVjq9HkNNI27eNf3eFPyCo";
        private static string path = Environment.CurrentDirectory + @"/" + "TelegramBot.txt";
        private static TelegramBotClient client;
        static void Main(string[] args)
        {
            Console.WriteLine("Bot is running...");
            client = new TelegramBotClient(token);
            client.StartReceiving();
            client.OnMessage += OnMessageHandler;
            Console.ReadLine();
            client.StopReceiving();
            
        }

        private static async void OnMessageHandler(object sender, MessageEventArgs e)   
        {
            var msg = e.Message;
            if (msg.Text != null)
            {
                Console.WriteLine($" {msg.From}: {msg.Text}");
                StreamWriter writer = new StreamWriter(path, true);
                writer.WriteLine($"{msg.From}: {msg.Text} ({DateTime.Now})" );
                writer.Close();
                

                switch (msg.Text)
                {
                    case "Стикер":
                        var stick = await client.SendStickerAsync(
                            chatId: msg.Chat.Id,
                            sticker: "https://cdn.tlgrm.ru/stickers/b48/7e2/b487e222-21cd-4741-b567-74b25f44b21a/192/3.webp",
                            replyToMessageId: msg.MessageId,
                            replyMarkup: GetButtons());
                        break;

                    case "Картинка":
                        var pic = await client.SendPhotoAsync(
                            chatId: msg.Chat.Id,
                            photo: "https://sun9-57.userapi.com/impg/4b7GLehH0kM2PkUP53sxOvf7d09pe49DedXAhQ/nOJWRVm7z_Q.jpg?size=1953x1255&quality=96&sign=14aee579f9e7b078f24e9301c69b75df&type=album",
                            replyToMessageId: msg.MessageId,
                            replyMarkup: GetButtons());
                        break;

                    case "Видео":

                        var vid = await client.SendVideoAsync(
                            chatId: msg.Chat.Id,
                            video: File.OpenRead(Environment.CurrentDirectory + @"/" + "vid.MP4"),
                            thumb: "https://sun9-57.userapi.com/impg/4b7GLehH0kM2PkUP53sxOvf7d09pe49DedXAhQ/nOJWRVm7z_Q.jpg?size=1953x1255&quality=96&sign=14aee579f9e7b078f24e9301c69b75df&type=album",
                            replyToMessageId: msg.MessageId,
                            supportsStreaming: true) ;
                        break;

                    case "Видео-ответ":
                        using (var stream = File.OpenRead(@"G:\Media\SomeVids\BXAJ0401.MP4"))
                        {
                            msg = await client.SendVideoNoteAsync(
                                chatId: msg.Chat.Id,
                                videoNote: stream,
                                duration: 30,
                                length: 180
                                );
                        }
                        break;

                    default:    
                        await client.SendTextMessageAsync(msg.Chat.Id, "Халяс пезер, остановись");
                        
                        break;
                }


                // Первый аргумент указывает, куда отправлять ответ, а второй аргумент - что отвечать.
                //await client.SendTextMessageAsync(msg.Chat.Id, "Ваше сообщение принято", replyMarkup: GetButtons());
            }
        }

        private static IReplyMarkup GetButtons()
        {
            return new ReplyKeyboardMarkup
            {
                Keyboard = new List<List<KeyboardButton>>
                {
                    new List<KeyboardButton> { new KeyboardButton { Text = "Видео" }, new KeyboardButton { Text = "Видео-ответ" }},
                    new List<KeyboardButton> { new KeyboardButton { Text = "Стикер" }, new KeyboardButton { Text = "Картинка" } }

                }
            };
        }
    }
}

