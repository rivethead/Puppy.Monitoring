using System;

namespace Puppy.Monitoring.Events
{
    [Serializable]
    public class NoopEvent : IEvent
    {
        public EventTiming EventAudit { get; private set; }
        public Categorisation Categorisation { get; private set; }
        public Timings Timings { get; private set; }
    }
}