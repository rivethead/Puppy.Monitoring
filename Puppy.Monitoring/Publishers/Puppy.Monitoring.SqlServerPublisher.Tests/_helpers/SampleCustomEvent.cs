using System;
using Puppy.Monitoring.Events;
using Puppy.Monitoring.Publishing;

namespace Puppy.Monitoring.SqlServerPublisher.Tests._helpers
{
    public class SampleCustomEvent : Event
    {
        public SampleCustomEvent(PublishingContext context, EventTiming eventAudit, Categorisation categorisation, Guid correlationId, Timings timings,
                                 Guid id) : base(context, eventAudit, categorisation, correlationId, timings, id)
        {
        }

        public SampleCustomEvent() : base(new Categorisation("unknown"), Guid.Empty)
        {
        }

        public SampleCustomEvent(DateTime publishedOn, Categorisation categorisation, Timings timings, Guid correlationId)
            : base(publishedOn, categorisation, timings, correlationId)
        {
        }

        public SampleCustomEvent(DateTime publishedOn, Categorisation categorisation, Timings timings)
            : base(publishedOn, categorisation, timings)
        {
            Context = new PublishingContext("TEST_SYSTEM", "TEST_MODULE");
        }
    }
}