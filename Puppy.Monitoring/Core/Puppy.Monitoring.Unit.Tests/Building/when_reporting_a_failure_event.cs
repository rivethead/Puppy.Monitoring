using System.Linq;
using Puppy.Monitoring.Events;
using Puppy.Monitoring.Publishing;
using Puppy.Monitoring.TestHelper;
using Xunit.Extensions;

namespace Puppy.Monitoring.Unit.Tests.Building
{
    public class when_reporting_a_failure_event : Specification
    {
        private readonly PublishedEventListener listener = new PublishedEventListener();

        public when_reporting_a_failure_event()
        {
            Publisher.Use(new TestPipelineAdapter(listener));
        }

        public override void Observe()
        {
            Report
                .Failure()
                .Publish();
        }

        [Observation]
        public void a_success_event_is_published()
        {
            listener.Events.First().ShouldBeType<FailureEvent>();
        }
    }
}