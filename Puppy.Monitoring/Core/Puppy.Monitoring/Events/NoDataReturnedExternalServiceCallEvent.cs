using System;
using Puppy.Monitoring.Publishing;

namespace Puppy.Monitoring.Events
{
    [Serializable]
    public class NoDataReturnedExternalServiceCallEvent : Event
    {
        public NoDataReturnedExternalServiceCallEvent(PublishingContext context, EventTiming eventAudit, Categorisation categorisation, Guid correlationId, Timings timings, Guid id)
            : base(context, eventAudit, categorisation, correlationId, timings, id)
        {
        }

        public NoDataReturnedExternalServiceCallEvent()
            : base(new Categorisation("unknown"), Guid.Empty)
        {
        }

        public NoDataReturnedExternalServiceCallEvent(Categorisation categorisation, Guid correlationId)
            : base(categorisation, correlationId)
        {
        }

        public NoDataReturnedExternalServiceCallEvent(Categorisation categorisation, Timings timings, Guid correlationId)
            : base(categorisation, timings, correlationId)
        {
        }

        public NoDataReturnedExternalServiceCallEvent(DateTime publishedOn, Categorisation categorisation, Timings timings)
            : base(publishedOn, categorisation, timings)
        {
        }

        public NoDataReturnedExternalServiceCallEvent(DateTime publishedOn, Categorisation categorisation, Timings timings, Guid correlationId)
            : base(publishedOn, categorisation, timings, correlationId)
        {
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