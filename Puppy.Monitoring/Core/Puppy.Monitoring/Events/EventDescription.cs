namespace Puppy.Monitoring.Events
{
    public class EventDescription
    {
        public string Parameters { get; internal set; }
        public string Description { get; internal set; }

        public EventDescription()
        {
        }

        public EventDescription(string parameters, string description)
        {
            Parameters = parameters;
            Description = description;
        }
    }
}