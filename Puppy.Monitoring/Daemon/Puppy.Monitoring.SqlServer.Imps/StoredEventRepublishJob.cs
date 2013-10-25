using System.Configuration;
using System.Data.SqlClient;
using Common.Logging;
using Puppy.Monitoring.Publishing;
using Quartz;

namespace Puppy.Monitoring.SqlServer.Imps
{
    public class StoredEventRepublishJob : IJob
    {
        private static readonly ILog log = LogManager.GetLogger<StoredEventRepublishJob>();
        private readonly IProviderStoredEvents eventProvider;
        private readonly IMarkPublishedEvents marker;

        public StoredEventRepublishJob(IProviderStoredEvents eventProvider, IMarkPublishedEvents marker)
        {
            this.eventProvider = eventProvider;
            this.marker = marker;
        }

        public StoredEventRepublishJob()
        {
            eventProvider = new SqlServerDatabaseStoredEvents();
            marker = new PublishedEventMarker(new SqlConnection(ConfigurationManager.ConnectionStrings["puppy.sqlserver"].ConnectionString));
        }

        public void Execute(IJobExecutionContext context)
        {
            var events = eventProvider.GetEvents();

            log.DebugFormat("Found {0} events to republish", events.Count);

            var publisher = new Publisher();

            foreach (var @event in events)
            {
                publisher.Publish(@event);
                marker.Mark(@event);
            }
        }
    }
}