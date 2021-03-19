using Messages;
using NServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DummyPublisher
{
    public class SendMessageHandler : IHandleMessages<SendMessageCommand>
    {
        private readonly IBus bus;

        public SendMessageHandler(IBus bus)
        {
            this.bus = bus;
        }
        public void Handle(SendMessageCommand message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"SendMessageCommand with id {message.EventId} received.");

            bus.Publish<MessageSentEvent>(m => m.EventId = message.EventId);

            Console.WriteLine("MessageSentEvent has been published");
            Console.ResetColor();
        }
    }
}
