using Puppy.Monitoring.Publishing;
using Quartz;

namespace Puppy.Monitoring.SqlServer.Imps
{
    public class StoredEventRepublishJob : IJob
    {
        private readonly IProviderStoredEvents eventProvider;

        public StoredEventRepublishJob(IProviderStoredEvents eventProvider)
        {
            this.eventProvider = eventProvider;
        }

        public StoredEventRepublishJob()
        {
            this.eventProvider = new SqlServerDatabaseStoredEvents();
        }

        public void Execute(IJobExecutionContext context)
        {
            var events = eventProvider.GetEvents();
            var publisher = new Publisher();

            foreach (var @event in events)
                publisher.Publish(@event);
        }


    }
}
