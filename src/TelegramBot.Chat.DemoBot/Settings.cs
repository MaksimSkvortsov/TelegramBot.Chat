using System;

namespace TelegramBot.Chat.DemoBot
{
    public class Settings
    {
        public static string OutputQueueName => Environment.GetEnvironmentVariable("OutputQueueName");

        public static string ServiceBusConnectionString => Environment.GetEnvironmentVariable("ServiceBusConnection");
    }
}
