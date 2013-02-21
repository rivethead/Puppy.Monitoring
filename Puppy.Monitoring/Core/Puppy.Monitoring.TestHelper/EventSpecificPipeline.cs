using System.Collections.Generic;
using System.Linq;
using Puppy.Monitoring.Events;
using Puppy.Monitoring.Pipeline;

namespace Puppy.Monitoring.TestHelper
{
    public class GenericPipeline : IPipeline
    {
        public GenericPipeline()
        {
            Events = new List<IEvent>();
        }

        public IList<IEvent> Events { get; private set; }

        public bool WasCalled
        {
            get { return Events.Any(); }
        }

        public void Flow(IEvent @event)
        {
            Events.Add(@event);
        }
    }

    public class EventSpecificPipeline : IPipeline<TestEvent>
    {
        public EventSpecificPipeline()
        {
            Events = new List<IEvent>();
        }

        public IList<IEvent> Events { get; private set; }

        public bool WasCalled
        {
            get { return Events.Any(); }
        }

        public void Flow(TestEvent @event)
        {
            Events.Add(@event);
        }

        public void Flow(IEvent @event)
        {
            Flow(@event as TestEvent);
        }
    }
}