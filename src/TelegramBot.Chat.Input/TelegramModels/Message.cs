using System;
using System.Collections.Generic;
using System.Text;

namespace TelegramBot.Chat.Input.TelegramModels
{
    internal class Message
    {
        public Chat Chat { get; set; }

        public string Text { get; set; }

        public string Date { get; set; }
    }
}
