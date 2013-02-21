using System;
using Puppy.Monitoring.Events;

namespace Puppy.Monitoring.TestHelper
{
    [Serializable]
    public class TestEvent : Event
    {
        public TestEvent() : base(SystemTime.Now(),
                                  new Categorisation("Category", "SubCategory"), new Timings(0))
        {
        }

        public TestEvent(Categorisation categorisation)
            : base(SystemTime.Now(), categorisation, new Timings(0))
        {
        }

        public TestEvent(Categorisation categorisation, Timings timings) : base(SystemTime.Now(), categorisation, timings)
        {
        }

        public string Description { get; set; }
    }
}