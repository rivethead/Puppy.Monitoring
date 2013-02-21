using Puppy.Monitoring.Events;

namespace Puppy.Monitoring.Adapters
{
    public interface IPipelineAdapter
    {
        void Push(IEvent @event);
    }
}