using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using TelegramBot.Chat.Output.Telegram;

namespace TelegramBot.Chat.Output
{
    public static class Function
    {
        [FunctionName("func-sendTelegramMessage")]
        public static async Task Run([ServiceBusTrigger("sbq-telegram-chat-output", Connection = "ServiceBusConnection")]string queueItem, ILogger log)
        {
            log.LogInformation($"C# ServiceBus queue trigger function processed message: {queueItem}");

            var queueItemModel = JsonConvert.DeserializeObject<QueueItem>(queueItem);

            var telegramClient = new TelegramClient(Settings.TelegramApiKey);
            var response = await telegramClient.SendMessageAsync(queueItemModel.ChatId, queueItemModel.Message);

            if (response.IsSuccessful)
            {
                log.LogInformation($"Telegram message delivery was requested. Message: {queueItem}.");
            }
            else
            {
                log.LogError($"Telegram message delivery request failed. Response status: {response.StatusCode}. Error: {response.Error}.");
            }
        }
    }
}
