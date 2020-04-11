using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot.Chat.Output.Telegram
{
    public class TelegramClient
    {
        private const string ContentMediaType = "application/json";
        private static Encoding ContentEncoding = Encoding.UTF8;
        
        private readonly string _apiKey;
        
        public TelegramClient(string apiKey) => _apiKey = apiKey;
        
        public async Task<Response> SendMessageAsync(string chatId, string message)
        {
            var url = $"https://api.telegram.org/bot{_apiKey}/sendMessage?chat_id={chatId}&amp";
            var messageData = new Message
            {
                ChatId = chatId,
                Text = message
            };

            using (var client = new HttpClient())
            {
                var content = new StringContent(JsonConvert.SerializeObject(messageData), ContentEncoding, ContentMediaType);
                var response = await client.PostAsync(url, content);

                string error = null;
                if (!response.IsSuccessStatusCode)
                {
                    error = await response.Content.ReadAsStringAsync();
                }
                return new Response((int)response.StatusCode, response.IsSuccessStatusCode, error);
            }
        }
    }
}
