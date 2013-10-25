using Puppy.Monitoring.Events;

namespace Puppy.Monitoring.SqlServer.Imps
{
    public interface IMarkPublishedEvents
    {
        void Mark(IEvent @event);
    }
}