using System;
using System.Collections.Generic;
using System.Text;

namespace TelegramBot.Classes
{
    public static class SomeFeatures
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string PasswordGenerator()
        {
            string Alpabet = "QWERTYUIOPASDFGHJKLZXCVBNM0123456789qwertyuiopasdfghjklzxcvbnm";
            Random rand = new Random();

            string password = "";
            for (int i = 0; i < 10; i++)
                password += Alpabet[rand.Next(Alpabet.Length)];
            return password;
        }
    }
}
