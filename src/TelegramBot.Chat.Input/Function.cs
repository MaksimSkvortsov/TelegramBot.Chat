using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

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

            string name = req.Query["name"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.name;

            string responseMessage = string.IsNullOrEmpty(name)
                ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
                : $"Hello, {name}. This HTTP triggered function executed successfully.";

            return new OkObjectResult(responseMessage);
        }
    }
}
