﻿using System;
using Puppy.Monitoring.Events;
using Puppy.Monitoring.Publishing;

namespace Puppy.Monitoring.TestHelper
{
    [Serializable]
    public class AnotherTestEvent : IEvent
    {
        public string Description { get; set; }
        public EventTiming EventAudit { get; private set; }
        public Categorisation Categorisation { get; private set; }
        public Timings Timings { get; private set; }
        public PublishingContext Context { get; private set; }
        public void AttachContext(PublishingContext context)
        {
            Context = context;
        }
    }
}