using System;
using Puppy.Monitoring.Events;
using Puppy.Monitoring.Publishing;

namespace Puppy.Monitoring.SqlServerPublisher.Tests._helpers
{
    public class SampleCustomEvent : Event
    {
        public SampleCustomEvent(DateTime publishedOn, Categorisation categorisation, Timings timings)
            : base(publishedOn, categorisation, timings)
        {
            Context = new PublishingContext("TEST_SYSTEM", "TEST_MODULE");
        }
    }
}