using System;
using Puppy.Monitoring.Events;

namespace Puppy.Monitoring.Unit.Tests._helpers
{
    public class SampleCustomEvent : Event
    {
        public SampleCustomEvent(DateTime publishedOn, Categorisation categorisation, Timings timings)
            : base(publishedOn, categorisation, timings)
        {
        }
    }
}