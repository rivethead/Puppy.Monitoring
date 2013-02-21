using System;

namespace Puppy.Monitoring.Events
{
    [Serializable]
    public class ActionTriggeredEvent : Event, IAdminEvent
    {
        public ActionTriggeredEvent(IEvent @event)
            : base(SystemTime.Now(), 
                    new Categorisation(string.Format("Triggers/{0}", @event.Categorisation.Category), @event.GetType().FullName), 
                    new Timings(0))
        {
        }
    }
}