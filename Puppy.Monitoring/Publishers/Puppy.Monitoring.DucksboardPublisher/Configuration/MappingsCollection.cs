using System.Configuration;

namespace Puppy.Monitoring.DucksboardPublisher.Configuration
{
    [ConfigurationCollection(typeof(MapElement))]
    public class MappingsCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new MapElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((MapElement)(element)).Counter;
        }

        [ConfigurationProperty("url", IsRequired = true, DefaultValue = "https://push.ducksboard.com/v/")]
        public string Url
        {
            get { return (string)base["url"]; }
            set { base["url"] = value; }
        }

        [ConfigurationProperty("apiKey", IsRequired = true, DefaultValue = "")]
        public string ApiKey
        {
            get { return (string)base["apiKey"]; }
            set { base["apiKey"] = value; }
        }

        public MapElement this[int idx]
        {
            get
            {
                return (MapElement)BaseGet(idx);
            }
        }
    }
}