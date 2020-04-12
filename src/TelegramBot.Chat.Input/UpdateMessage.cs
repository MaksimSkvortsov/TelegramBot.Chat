using System;
using System.Collections.Generic;
using System.Text;

namespace TelegramBot.Chat.Input
{
    public class UpdateMessage
    {
        public string UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Message { get; set; }
    }
}
