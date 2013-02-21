namespace Puppy.Monitoring.Events
{
    public class NullReportEvent : IEvent
    {
        public EventTiming EventAudit
        {
            get { return null; }
        }

        public Categorisation Categorisation
        {
            get { return null; }
        }

        public Timings Timings
        {
            get { return null; }
        }
    }
}