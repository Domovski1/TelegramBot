using System;
using System.IO;
using VideoLibrary;

namespace TelegramBot.Classes
{
    public static class VideoFromYoutubeToAudio
    {
        /// <summary>
        /// Переменная, содержащая абсолютный путь к файлу
        /// </summary>
        public static string FileName { get; set; 
        
        }
        /// <summary>
        /// Обработчик видео в mp3
        /// </summary>
        /// <param name="path">Принимает путь к файлу</param>
        public static void Conv(string path)
        {
            try
            {
                var ConvertVideo = new NReco.VideoConverter.FFMpegConverter();
                ConvertVideo.ConvertMedia(path, path + ".mp3", "mp3");
                Console.WriteLine($"[INFO] Video was converted in mp3");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }



        }


        /// <summary>
        /// Загрузчик видео из Youtube.com
        /// </summary>
        /// <param name="link">Принимает ссылку на видео из Youtube</param>
        public static void SaveVideoToDisk(string link)
        {
            try
            {
                var youTube = YouTube.Default; // Начальная точка для начала работы с Youtube
                var video = youTube.GetVideo(link); // Получаем видео со всей информацией
                FileName = @"C:\Users\62427\Desktop\Bot\" + video.FullName;
                File.WriteAllBytes(FileName, video.GetBytes());
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

// Документация
// https://telegrambots.github.io/book/index.html