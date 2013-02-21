using System;
using System.Linq;
using Puppy.Monitoring.Publishing;
using Puppy.Monitoring.TestHelper;
using Xunit.Extensions;

namespace Puppy.Monitoring.Unit.Tests.Reporting.CustomEvent
{
    public class when_reporting_a_custom_event : Specification
    {
        private readonly PublishedEventListener publisher = new PublishedEventListener();

        public when_reporting_a_custom_event()
        {
            SystemTime.Now = () => new DateTime(2013, 04, 20, 12, 13, 14);

            Publisher.Use(new TestPipelineAdapter(publisher));
        }

        public override void Observe()
        {
            Report
                .CustomEvent()
                .Event(new TestEvent())
                .Publish();
        }

        [Observation]
        public void the_custom_event_is_published()
        {
            publisher.Events.First().ShouldBeType<TestEvent>();
        }

    }
}