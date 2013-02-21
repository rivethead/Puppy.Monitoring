using Puppy.Monitoring.Adapters;
using Puppy.Monitoring.Events;
using Puppy.Monitoring.Publishing;

namespace Puppy.Monitoring.TestHelper
{
    public class TestPipelineAdapter : IPipelineAdapter
    {
        private readonly IPublishEvents publisher;

        public TestPipelineAdapter() : this(new PublishedEventListener())
        {
        }

        public TestPipelineAdapter(IPublishEvents publisher)
        {
            this.publisher = publisher;
        }

        public void Push(IEvent @event)
        {
            publisher.Publish(@event);
        }
    }
}