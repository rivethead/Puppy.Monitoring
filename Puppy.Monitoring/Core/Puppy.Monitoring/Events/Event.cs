using System;
using Puppy.Monitoring.Publishing;

namespace Puppy.Monitoring.Events
{
    [Serializable]
    public abstract class Event : IEvent
    {
        protected Event()
        {
            Description = new EventDescription();
        }

        protected Event(Categorisation categorisation, Guid correlationId)
            : this(SystemTime.Now(), categorisation, null, correlationId)
        {
        }

        protected Event(Categorisation categorisation, Guid correlationId, EventDescription description)
            : this(SystemTime.Now(), categorisation, null, correlationId, description)
        {
        }

        protected Event(Categorisation categorisation, Timings timings, Guid correlationId)
            : this(SystemTime.Now(), categorisation, timings, correlationId, new EventDescription())
        {
        }

        protected Event(Categorisation categorisation, Timings timings, Guid correlationId, EventDescription description)
            : this(SystemTime.Now(), categorisation, timings, correlationId, description)
        {
        }

        protected Event(DateTime publishedOn, Categorisation categorisation, Timings timings) : this(publishedOn, categorisation, timings, Guid.Empty)
        {
        }

        protected Event(DateTime publishedOn, Categorisation categorisation, Timings timings, EventDescription description) : this(publishedOn, categorisation, timings, Guid.Empty, description)
        {
        }

        protected Event(PublishingContext context, EventTiming eventAudit, Categorisation categorisation, Guid correlationId, Timings timings, Guid id)
            : this(context, eventAudit, categorisation, correlationId, timings, id, new EventDescription())
        {
        }

        protected Event(PublishingContext context, EventTiming eventAudit, Categorisation categorisation, Guid correlationId, Timings timings, Guid id,
            EventDescription description)
        {
            Context = context;
            EventAudit = eventAudit;
            Categorisation = categorisation;
            CorrelationId = correlationId;
            Timings = timings;
            Id = id;
            Description = description;
        }

        protected Event(DateTime publishedOn, Categorisation categorisation, Timings timings, Guid correlationId)
            : this(publishedOn, categorisation, timings, correlationId, new EventDescription())
        {
        }

        protected Event(DateTime publishedOn, Categorisation categorisation, Timings timings, Guid correlationId, EventDescription description)
        {
            Categorisation = categorisation;
            Timings = timings;
            CorrelationId = correlationId;
            Description = description;
            EventAudit = new EventTiming(publishedOn);
            Id = Guid.NewGuid();
        }

        public EventDescription Description { get; internal set; }

        public Guid Id { get; internal set; }

        public PublishingContext Context { get; protected set; }
        public EventTiming EventAudit { get; internal set; }
        public Categorisation Categorisation { get; internal set; }
        public Guid CorrelationId { get; internal set; }
        public Timings Timings { get; internal set; }

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