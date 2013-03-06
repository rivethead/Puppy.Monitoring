using Puppy.Monitoring.Events;
using Puppy.Monitoring.Publishing;

namespace Puppy.Monitoring.DucksboardPublisher
{
    public class DucksboardPublisher : IPublishEvents
    {
        private readonly string apiKey;

        public DucksboardPublisher(string apiKey)
        {
            this.apiKey = apiKey;
        }

        public void Publish(IEvent @event)
        {
            
        }
    }
}