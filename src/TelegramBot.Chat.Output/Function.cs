using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using TelegramBot.Chat.Contract;
using TelegramBot.Chat.Output.Telegram;

namespace TelegramBot.Chat.Output
{
    public static class Function
    {
        [FunctionName("func-sendTelegramMessage")]
        public static async Task Run([ServiceBusTrigger("sbq-telegram-chat-output", Connection = "ServiceBusConnection")]ReplyQueueMessage queueItem, ILogger log)
        {
            var telegramClient = new TelegramClient(Settings.TelegramApiKey);
            var response = await telegramClient.SendMessageAsync(queueItem.ChatId, queueItem.Message);

            if (response.IsSuccessful)
            {
                log.LogInformation($"Telegram message delivery was requested. Chat ID: {queueItem.ChatId}.");
            }
            else
            {
                log.LogError($"Telegram message delivery request failed. Chat ID: {queueItem.ChatId}, response status: {response.StatusCode}, error: {response.Error}.");
            }
        }
    }
}
