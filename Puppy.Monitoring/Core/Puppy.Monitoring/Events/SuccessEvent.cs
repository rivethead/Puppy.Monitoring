using System;

namespace Puppy.Monitoring.Events
{
    [Serializable]
    public class SuccessEvent : Event
    {
        public SuccessEvent(Categorisation categorisation, Timings timings)
            : base(SystemTime.Now(), categorisation, timings)
        {
        }

        public SuccessEvent(Categorisation categorisation)
            : base(SystemTime.Now(), categorisation, new Timings(int.MinValue))
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