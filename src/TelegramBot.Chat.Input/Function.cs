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
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "Update/{token}")] HttpRequest req,
            string token,
            ILogger log)
        {
            //Better replace with API gateway validation
            if (token != Settings.TelegramToken)
            {
                log.LogError($"Telegram POST request received with invalid token. Token = {token}.");
                return new UnauthorizedResult();
            }
            log.LogInformation($"Telegram POST request received.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var data = JsonConvert.DeserializeObject<TelegramUpdate>(requestBody);

            var queueMessage = CreateQueueMessage(data.Message);

            return new OkObjectResult(queueMessage);
        }

        private static UpdateMessage CreateQueueMessage(Message telegramMessage)
        {
            var chat = telegramMessage.Chat;

            return new UpdateMessage
            {
                UserId = chat.Id,
                FirstName = chat.FirstName,
                LastName = chat.LastName,
                Message = telegramMessage.Text
            };
        }
    }
}
