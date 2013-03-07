using System.Configuration;

namespace Puppy.Monitoring.DucksboardPublisher.Configuration
{
    public class MapElement : ConfigurationElement
    {
        [ConfigurationProperty("counter", IsRequired = true)]
        public int Counter
        {
            get { return (int) (base["counter"]); }
            set { base["counter"] = value; }
        }

        [ConfigurationProperty("category", IsRequired = true)]
        public string Category
        {
            get
            {
                return ((string)(base["category"]));
            }
            set
            {
                base["category"] = value;
            }
        }

        [ConfigurationProperty("payload", IsRequired = true)]
        public string Payload
        {
            get
            {
                return ((string)(base["payload"]));
            }
            set
            {
                base["payload"] = value;
            }
        }

        [ConfigurationProperty("slotNumber", IsRequired = true)]
        public int SlotNumber
        {
            get
            {
                return ((int)(base["slotNumber"]));
            }
            set
            {
                base["slotNumber"] = value;
            }
        }

        [ConfigurationProperty("subCategory", IsRequired = false)]
        public string SubCategory
        {
            get
            {
                return ((string)(base["subCategory"]));
            }
            set
            {
                base["subCategory"] = value;
            }
        }

    }
}