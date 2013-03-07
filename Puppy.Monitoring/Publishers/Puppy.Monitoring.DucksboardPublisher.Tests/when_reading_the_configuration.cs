using System;
using System.Collections.Generic;
using System.Linq;
using Puppy.Monitoring.DucksboardPublisher.Configuration;
using Xunit.Extensions;

namespace Puppy.Monitoring.DucksboardPublisher.Tests
{
    public class when_reading_the_configuration : Specification
    {
        private Exception actual_exception;
        private IEnumerable<MapElement> configuration;

        public override void Observe()
        {
            try
            {
                configuration = EventMappingConfigSection.GetMapping();
            }
            catch (Exception e)
            {
                actual_exception = e;
            }
        }

        [Observation]
        public void the_mapping_is_loaded_from_the_configuration()
        {
            actual_exception.ShouldBeNull();
        }

        [Observation]
        public void the_mappings_are_available_for_querying()
        {
            configuration.Count().ShouldEqual(2);
        }
    }
}