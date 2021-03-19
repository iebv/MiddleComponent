
namespace MiddleComponent
{
    using NServiceBus;

    public class EndpointConfig : IConfigureThisEndpoint
    {
        public void Customize(BusConfiguration configuration)
        {
            configuration.EndpointName("middlecomponent");
            configuration.UseTransport<MsmqTransport>();
            configuration.UseSerialization<JsonSerializer>();
            configuration.UsePersistence<InMemoryPersistence>();

            ConventionsBuilder conventions = configuration.Conventions();
            conventions.DefiningCommandsAs(c => c.Namespace != null && c.Namespace == "Messages" && c.Name.EndsWith("Command"));
            conventions.DefiningEventsAs(c => c.Namespace != null && c.Namespace == "Messages" && c.Name.EndsWith("Event"));
        }
    }
}
