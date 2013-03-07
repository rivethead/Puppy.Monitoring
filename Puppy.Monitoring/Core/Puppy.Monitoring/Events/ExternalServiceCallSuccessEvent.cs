using System;

namespace Puppy.Monitoring.Events
{
    [Serializable]
    public class ExternalServiceCallSuccessEvent : Event
    {
        public ExternalServiceCallSuccessEvent() : base(new Categorisation("unknown"), Guid.Empty)
        {
        }

        public ExternalServiceCallSuccessEvent(Categorisation categorisation, Guid correlationId) : base(categorisation, correlationId)
        {
        }

        public ExternalServiceCallSuccessEvent(Categorisation categorisation, Timings timings, Guid correlationId)
            : base(categorisation, timings, correlationId)
        {
        }

        public ExternalServiceCallSuccessEvent(DateTime publishedOn, Categorisation categorisation, Timings timings)
            : base(publishedOn, categorisation, timings)
        {
        }

        public ExternalServiceCallSuccessEvent(DateTime publishedOn, Categorisation categorisation, Timings timings, Guid correlationId)
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