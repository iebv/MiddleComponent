using Messages;
using NServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            BusConfiguration configuration = new BusConfiguration();
            configuration.UseTransport<MsmqTransport>();
            configuration.UseSerialization<JsonSerializer>();

            ConventionsBuilder conventions = configuration.Conventions();
            conventions.DefiningCommandsAs(c => c.Namespace != null && c.Namespace == "Messages" && c.Name.EndsWith("Command"));

            using (ISendOnlyBus bus = Bus.CreateSendOnly(configuration))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("When you're ready, press 1 to send the command!");
                Console.WriteLine("Press any other key to exit");
               
                while (true)
                {

                    var keyPressed = Console.ReadKey();
                    Console.WriteLine();

                    if (keyPressed.Key == ConsoleKey.D1)
                    {
                        SendMessageCommand cmd = new SendMessageCommand() { EventId = Guid.NewGuid().ToString() };
                        Console.WriteLine($"Sending SendMessageCommand with id {cmd.EventId}");
                        bus.Send(cmd);
                    }
                    else
                    {
                        return;
                    }
                }    
            }

        }
    }
}
