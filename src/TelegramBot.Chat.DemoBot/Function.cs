using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using TelegramBot.Chat.Contract;

namespace TelegramBot.Chat.DemoBot
{
    public static class Function
    {
        [FunctionName("func-demoBot")]
        public static async Task Run([ServiceBusTrigger("sbq-telegram-chat-input", Connection = "ServiceBusConnection")]UpdateQueueMessage queueMessage, ILogger log)
        {
            var replyMessage = new ReplyQueueMessage
            {
                ChatId = queueMessage.ChatId,
                Message = queueMessage.Message
            };

            var telegramClient = new TelegramQueueClient(Settings.ServiceBusConnectionString, Settings.OutputQueueName);
            await telegramClient.SendAsync(replyMessage);

            log.LogDebug($"Demo bot processed message from chat: {queueMessage.ChatId}");
        }
    }
}
