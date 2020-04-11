using System;

namespace TelegramBot.Chat.Output
{
    public static class Settings
    {
        public static string TelegramApiKey => Environment.GetEnvironmentVariable("TelegramApiKey");
    }
}
