using Puppy.Monitoring.Events;

namespace Puppy.Monitoring.DucksboardPublisher
{
    internal interface IDucksboardPayload
    {
        string GeneratePayload(IEvent @event);
    }
}