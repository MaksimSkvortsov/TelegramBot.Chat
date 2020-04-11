using Newtonsoft.Json;

namespace TelegramBot.Chat.Output.Telegram
{
    internal class Message
    {
        [JsonProperty("chat_id")]
        public string ChatId { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }
    }
}
