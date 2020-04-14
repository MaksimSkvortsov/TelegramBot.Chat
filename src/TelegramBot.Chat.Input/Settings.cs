using System;

namespace TelegramBot.Chat.Input
{
    public static class Settings
    {
        public static string TelegramToken => Environment.GetEnvironmentVariable("TelegramToken");

        public static string InputQueueName => Environment.GetEnvironmentVariable("InputQueueName");

        public static string ServiceBusConnectionString => Environment.GetEnvironmentVariable("ServiceBusConnectionString");
    }
}
