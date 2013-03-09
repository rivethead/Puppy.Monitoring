using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Puppy.Monitoring.DucksboardPublisher.Configuration
{
    public class EventMappingConfigSection : ConfigurationSection
    {
        [ConfigurationProperty("Mappings")]
        public MappingsCollection MappingItems
        {
            get { return ((MappingsCollection)(base["Mappings"])); }
        }

        public static string GetUrl()
        {
            return (ConfigurationManager.GetSection("EventMapping") as EventMappingConfigSection).MappingItems.Url;
        }

        public static string GetApiKey()
        {
            return (ConfigurationManager.GetSection("EventMapping") as EventMappingConfigSection).MappingItems.ApiKey;
        }

        public static IEnumerable<MapElement> GetMapping()
        {
            var items = (ConfigurationManager.GetSection("EventMapping") as EventMappingConfigSection).MappingItems;

            return from object item in items select item as MapElement;
        }
    }
}