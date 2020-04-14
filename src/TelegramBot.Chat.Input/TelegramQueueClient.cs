using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot.Chat.Input
{
    public class TelegramQueueClient
    {
        private readonly QueueClient _queueClient;


        public TelegramQueueClient(string serviceBusConnection, string queueName)
        {
            _queueClient = new QueueClient(serviceBusConnection, queueName);
        }

        public async Task SendAsync(UpdateMessage queueItem)
        {
            var byteContent = SerializeToBinary(queueItem);
            await _queueClient.SendAsync(new Message(byteContent));
        }


        private static byte[] SerializeToBinary(object value)
        {
            var content = JsonConvert.SerializeObject(value);
            return Encoding.UTF8.GetBytes(content);
        }
    }
}
