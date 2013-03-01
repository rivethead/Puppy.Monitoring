using Puppy.Monitoring.Publishing;

namespace Puppy.Monitoring.Events
{
    public interface IEvent
    {
        EventTiming EventAudit { get; }
        Categorisation Categorisation { get; }
        Timings Timings { get; }
        PublishingContext Context { get; }
        void AttachContext(PublishingContext context);
    }
}