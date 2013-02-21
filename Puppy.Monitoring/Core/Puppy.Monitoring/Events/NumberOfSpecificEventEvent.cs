namespace Puppy.Monitoring.Events
{
    public class NumberOfSpecificEventEvent : Event, IAdminEvent
    {
        public NumberOfSpecificEventEvent(int number, IEvent @event)
            : base(SystemTime.Now(), new Categorisation("NumberOfEvents", @event.GetType().FullName), new Timings(0))
        {
            Number = number;
        }

        public int Number { get; private set; }
    }
}