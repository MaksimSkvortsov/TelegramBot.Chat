using System;

namespace TelegramBot.Chat.Input
{
    public static class Settings
    {
        public static string TelegramToken => Environment.GetEnvironmentVariable("TelegramToken");
    }
}
