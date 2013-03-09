using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Common.Logging;
using Puppy.Monitoring.DucksboardPublisher.Configuration;
using Puppy.Monitoring.Events;
using Puppy.Monitoring.Publishing;

namespace Puppy.Monitoring.DucksboardPublisher
{
    public class DucksboardPublisher : IPublishEvents
    {
        private readonly string apiKey;
        private readonly IList<MapElement> mappings;
        private readonly string url;
        private static readonly ILog log = LogManager.GetLogger<DucksboardPublisher>();

        public DucksboardPublisher()
        {
            apiKey = EventMappingConfigSection.GetApiKey();
            url = EventMappingConfigSection.GetUrl();
            mappings = EventMappingConfigSection.GetMapping().ToList();
        }

        public void Publish(IEvent @event)
        {
            var matches = mappings
                .Where(
                    m => m.Category.Equals(@event.Categorisation.Category, StringComparison.InvariantCultureIgnoreCase) &&
                        m.SubCategory.Equals(@event.Categorisation.SubCategory, StringComparison.InvariantCultureIgnoreCase)
                );

            Publish(@event, matches);
        }

        private void Publish(IEvent @event, IEnumerable<MapElement> mappingMatches)
        {
            foreach (var mappingMatch in mappingMatches)
            {
                var payloadType = Type.GetType(mappingMatch.Payload);
                var payloadGenerator = Activator.CreateInstance(payloadType) as IDucksboardPayload;

                var payload = payloadGenerator.GeneratePayload(@event);

                log.InfoFormat("Publishing {0} to Ducksboard", @event.GetType());
                Publish(payload, mappingMatch);
            }
        }

        private void Publish(string payload, MapElement mappingMatch)
        {
            var ducksBoardUrl = string.Format("{0}{1}", url, mappingMatch.SlotNumber);

            var client = new WebClient
            {
                Credentials = new CredentialCache
                        {
                            {
                                new Uri(url),
                                "Basic",
                                new NetworkCredential(apiKey, "~")
                            }
                        },
                BaseAddress = url,
            };

            client.Headers.Add("Content-Type", "application/json");

            log.DebugFormat("Publishing to {0}", ducksBoardUrl);
            var s = client.UploadString(new Uri(ducksBoardUrl), payload);

            Console.WriteLine(s);

        }
    }
}