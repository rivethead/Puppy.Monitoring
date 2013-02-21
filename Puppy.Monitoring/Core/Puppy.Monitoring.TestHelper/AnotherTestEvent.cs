using System;
using Puppy.Monitoring.Events;

namespace Puppy.Monitoring.TestHelper
{
    [Serializable]
    public class AnotherTestEvent : IEvent
    {
        public string Description { get; set; }
        public EventTiming EventAudit { get; private set; }
        public Categorisation Categorisation { get; private set; }
        public Timings Timings { get; private set; }
    }
}