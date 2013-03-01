using System.Linq;
using Puppy.Monitoring.Events;
using Puppy.Monitoring.Publishing;
using Puppy.Monitoring.TestHelper;
using Xunit.Extensions;

namespace Puppy.Monitoring.Unit.Tests.Building
{
    public class when_reporting_a_success_event : Specification
    {
        private readonly PublishedEventListener listener = new PublishedEventListener();

        public when_reporting_a_success_event()
        {
            Publisher.Use(new TestPipelineAdapter(listener), new PublishingContext("TEST_SYSTEM", "TEST"));
        }

        public override void Observe()
        {
            Report
                .Success()
                .Publish();
        }

        [Observation]
        public void a_success_event_is_published()
        {
            listener.Events.First().ShouldBeType<SuccessEvent>();
        }
    }
}
