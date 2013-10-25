using System;
using System.Collections.Generic;
using Moq;
using Puppy.Monitoring.Events;
using Xunit.Extensions;

namespace Puppy.Monitoring.SqlServer.Imps.Tests
{
    public class when_republishing_stored_events : Specification
    {
        private readonly StoredEventRepublishJob republish_job;
        private readonly Mock<IProviderStoredEvents> event_provider;
        private readonly Mock<IMarkPublishedEvents> marker;

        public when_republishing_stored_events()
        {
            event_provider = new Mock<IProviderStoredEvents>();
            event_provider
                .Setup(ep => ep.GetEvents())
                .Returns(new List<IEvent>()
                    {
                        new SqlServerImpTestEvent(new Categorisation("TEST"), Guid.Empty),
                    });

            marker = new Mock<IMarkPublishedEvents>();

            republish_job = new StoredEventRepublishJob(event_provider.Object, marker.Object);
        }

        public override void Observe()
        {
            republish_job.Execute(null);
        }

        [Observation]
        public void the_events_provided_is_republished()
        {
            
        }

        [Observation]
        public void the_republished_events_are_marked_as_published()
        {
            
        }


    }
}
