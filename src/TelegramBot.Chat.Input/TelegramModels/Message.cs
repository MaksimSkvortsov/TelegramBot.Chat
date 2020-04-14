namespace TelegramBot.Chat.Input.TelegramModels
{
    internal class Message
    {
        public Chat Chat { get; set; }

        public Sender From { get; set; }

        public string Text { get; set; }

        public string Date { get; set; }
    }
}
