using System;
using Puppy.Monitoring.Publishing;

namespace Puppy.Monitoring.Events
{
    [Serializable]
    public abstract class Event : IEvent
    {
        protected Event(DateTime publishedOn, Categorisation categorisation, Timings timings)
        {
            Categorisation = categorisation;
            Timings = timings;
            EventAudit = new EventTiming(publishedOn);
        }

        public PublishingContext Context { get; private set; }
        public EventTiming EventAudit { get; private set; }
        public Categorisation Categorisation { get; private set; }
        public Timings Timings { get; private set; }

        public void AttachContext(PublishingContext context)
        {
            this.Context = context;
        }

        public override string ToString()
        {
            return string.Format("{0}{3}{1}{3}{2}",
                                 Categorisation,
                                 EventAudit,
                                 Timings,
                                 Environment.NewLine);
        }
    }
}