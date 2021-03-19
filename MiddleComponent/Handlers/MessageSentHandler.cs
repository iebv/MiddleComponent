using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Producer;
using Messages;
using NServiceBus;
using System;
using System.Text;
using System.Threading.Tasks;

namespace MiddleComponent
{
    public class MessageSentHandler : IHandleMessages<MessageSentEvent>
    {
        public void Handle(MessageSentEvent message)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"MessageSentEvent with id {message.EventId} received.");
            Console.WriteLine($"Sending Event to EH...");
            SendEventToEHAsync(message);
            Console.ResetColor();
        }

        private async Task SendEventToEHAsync(MessageSentEvent message)
        {
            const string eventHubName = "myeventhub";
            const string connectionString = "Endpoint=sb:";


            var producer = new EventHubProducerClient(connectionString, eventHubName);
 
            try
            {
                using (EventDataBatch eventBatch = await producer.CreateBatchAsync())
                {
                    eventBatch.TryAdd(new EventData(Encoding.UTF8.GetBytes(message.EventId)));
                    await producer.SendAsync(eventBatch);
                }
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"Event with id {message.EventId} successfully sent to EH.");
                
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(e.Message);
            }
            
 
            
        }
    }
}
