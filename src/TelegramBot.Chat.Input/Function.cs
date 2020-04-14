using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using TelegramBot.Chat.Input.TelegramModels;

namespace TelegramBot.Chat.Input
{
    public static class Function
    {
        [FunctionName("func-receiveTelegramMessage")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "Update/{token}")] HttpRequest request,
            string token,
            ILogger log)
        {
            //Better replace with API gateway validation
            if (token != Settings.TelegramToken)
            {
                log.LogError($"Telegram update received with invalid token. Token = {token}.");
                return new UnauthorizedResult();
            }
            log.LogInformation($"Telegram update received.");

            var telegramMessage = await GetRequestMessage(request);
            var queueMessage = CreateQueueMessage(telegramMessage.Message);

            var queueClient = new TelegramQueueClient(Settings.ServiceBusConnectionString, Settings.InputQueueName);
            await queueClient.SendAsync(queueMessage);

            return new AcceptedResult();
        }


        private static async Task<TelegramUpdate> GetRequestMessage(HttpRequest req)
        {
            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            return JsonConvert.DeserializeObject<TelegramUpdate>(requestBody);
        }

        private static UpdateMessage CreateQueueMessage(Message telegramMessage)
        {
            var sender = telegramMessage.From;

            return new UpdateMessage
            {
                ChatId = telegramMessage.Chat.Id,
                UserId = sender.Id,
                FirstName = sender.FirstName,
                LastName = sender.LastName,
                Message = telegramMessage.Text,
                LanguageCode = sender.LanguageCode,
                IsBot = sender.IsBot
            };
        }
    }
}
