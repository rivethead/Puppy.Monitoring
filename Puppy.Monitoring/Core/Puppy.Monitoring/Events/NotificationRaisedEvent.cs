namespace Puppy.Monitoring.Events
{
    public class NotificationRaisedEvent : Event, IAdminEvent
    {
        public NotificationRaisedEvent(IEvent @event) :
            base(SystemTime.Now(),
                 new Categorisation(string.Format("Notification/{0}", @event.Categorisation.Category), @event.GetType().FullName),
                 new Timings(0))
        {
        }
    }
}