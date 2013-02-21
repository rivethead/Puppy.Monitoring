using System;

namespace Puppy.Monitoring.Events
{
    [Serializable]
    public class Categorisation
    {
        public Categorisation(string category) : this(category, "<unknown>")
        {
        }

        public Categorisation(string category, string subCategory)
        {
            Category = category;
            SubCategory = subCategory;
        }

        public string Category { get; private set; }
        public string SubCategory { get; private set; }

        public override string ToString()
        {
            return string.Format("{0}/{1}", Category, SubCategory);
        }
    }
}