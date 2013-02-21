using System;

namespace Puppy.Monitoring.Events
{
    [Serializable]
    public class NumberOfEventsEvent : Event, IAdminEvent
    {
        public NumberOfEventsEvent(int number)
            : base(SystemTime.Now(), new Categorisation("NumberOfEvents", "All"), new Timings(0))
        {
            Number = number;
        }

        public int Number { get; private set; }
    }
}