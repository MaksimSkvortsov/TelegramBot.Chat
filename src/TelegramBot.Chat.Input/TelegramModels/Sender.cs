using Newtonsoft.Json;

namespace TelegramBot.Chat.Input.TelegramModels
{
    public class Sender
    {
        public string Id { get; set; }

        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("last_name")]
        public string LastName { get; set; }

        [JsonProperty("language_code")]
        public string LanguageCode { get; set; }

        [JsonProperty("is_bot")]
        public bool IsBot { get; set; }
    }
}
