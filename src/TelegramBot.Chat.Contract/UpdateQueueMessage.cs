namespace TelegramBot.Chat.Contract
{
    public class UpdateQueueMessage
    {
        public string ChatId { get; set; }

        public string UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Message { get; set; }

        public string LanguageCode { get; set; }

        public bool IsBot { get; set; }
    }
}
