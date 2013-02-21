namespace Puppy.Monitoring.Events
{
    public interface IEvent
    {
        EventTiming EventAudit { get; }
        Categorisation Categorisation { get; }
        Timings Timings { get; }
    }
}