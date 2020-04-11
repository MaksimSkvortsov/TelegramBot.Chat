using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace TelegramBot.Chat.Output
{
    public static class Function
    {
        [FunctionName("func-sendTelegramMessage")]
        public static void Run([ServiceBusTrigger("sbq-telegram-chat-output", Connection = "ServiceBusConnection")]string myQueueItem, ILogger log)
        {
            log.LogInformation($"C# ServiceBus queue trigger function processed message: {myQueueItem}");
        }
    }
}
