using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Skinny.Monitoring.Events;
using Skinny.Monitoring.Publishing;

namespace Skinny.Monitoring.DefaultPublishers
{
    public class BinaryFilePublisher : IPublishEvents
    {
        private readonly string filePath;

        public BinaryFilePublisher(string filePath)
        {
            this.filePath = filePath;
        }

        public void Publish(IEvent @event)
        {
            if (@event == null)
                throw new ArgumentNullException("event", "Event is null and cannot be published");

            using (var stream = new FileStream(filePath, FileMode.OpenOrCreate))
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(stream, @event);
            }
        }
    }
}