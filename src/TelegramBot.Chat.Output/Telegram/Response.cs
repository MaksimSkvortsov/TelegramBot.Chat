using System;

namespace TelegramBot.Chat.Output.Telegram
{
    public class Response
    {
        public Response(int statusCode, bool isSuccessful, string error = null)
        {
            if (isSuccessful && error != null)
                throw new ArgumentException("Error can be defined only for failed response.");

            StatusCode = statusCode;
            IsSuccessful = isSuccessful;
            Error = error;
        }

        public int StatusCode { get; }
        public bool IsSuccessful { get; }
        public string Error { get; }
    }
}
