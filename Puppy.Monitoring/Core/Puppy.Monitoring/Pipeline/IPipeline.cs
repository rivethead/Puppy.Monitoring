using Puppy.Monitoring.Events;

namespace Puppy.Monitoring.Pipeline
{
    public interface IPipeline
    {
        void Flow(IEvent @event);
    }

    public interface IPipeline<in TEvent> : IPipeline where TEvent : IEvent
    {
        void Flow(TEvent @event);
    }
}