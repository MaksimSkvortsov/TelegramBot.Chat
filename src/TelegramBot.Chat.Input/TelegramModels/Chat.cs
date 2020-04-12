using Newtonsoft.Json;

namespace TelegramBot.Chat.Input.TelegramModels
{
    internal class Chat
    {
        public string Id { get; set; }

        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("last_name")]
        public string LastName { get; set; }
    }
}
