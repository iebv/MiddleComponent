# MiddleComponent
This solution has a console app that sends a command to a Publisher which publishes an event to be read by the MiddleComponent subscriber. All this using NServiceBus.
The middle component sends the events to an EH after they are received.
