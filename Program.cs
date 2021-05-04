using System;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace TelegramBot
{
    class Program
    {
        private static string token { get; set; } = "1777573276:AAE7ePm8ZTn4rSVjq9HkNNI27eNf3eFPyCo";
        private static TelegramBotClient client;
        static void Main(string[] args)
        {
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
                Console.WriteLine($"Пришло сообщение: {msg.Text}");

                // Первый аргумент указывает, куда отправлять ответ, а второй аргумент - что отвечать.
                await client.SendTextMessageAsync(msg.Chat.Id, "Ваше сообщение принято");
            }
        }
    }
}
