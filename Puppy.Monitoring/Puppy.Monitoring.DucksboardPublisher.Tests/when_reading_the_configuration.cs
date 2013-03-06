using System.Configuration;
using Puppy.Monitoring.DucksboardPublisher.Configuration;
using Xunit.Extensions;

namespace Puppy.Monitoring.DucksboardPublisher.Tests
{
    public class when_reading_the_configuration : Specification
    {
        private PageAppearanceSection configuration;

        public override void Observe()
        {
            //configuration = ConfigurationManager.GetSection("pageAppearanceGroup/pageAppearance") as PageAppearanceSection;
        }

        [Observation]
        public void do_stuff()
        {
        }

    }
}
