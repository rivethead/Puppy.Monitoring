using System;
using Skinny.Monitoring.Events;
using Skinny.Monitoring.Publishing;

namespace Skinny.Monitoring.DefaultPublishers
{
    public class ConsolePublisher : IPublishEvents
    {
        public void Publish(IEvent @event)
        {
            Console.WriteLine("========================================");
            Console.WriteLine("Publishing event {0}", @event.GetType());
            Console.WriteLine("Event {0}", @event);
            Console.WriteLine("========================================");
        }
    }
}