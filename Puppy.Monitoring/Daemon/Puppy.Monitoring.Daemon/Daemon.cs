using System.Collections.Specialized;
using System.Configuration;
using Common.Logging;
using Quartz;
using Quartz.Impl;

namespace Puppy.Monitoring.Daemon
{
    public class Daemon
    {
        private static readonly ILog log = LogManager.GetLogger<Daemon>();
        private StdSchedulerFactory schedulerFactory;
        private IScheduler scheduler;

        public void Start()
        {
            log.InfoFormat("Starting the agent");

            schedulerFactory = new StdSchedulerFactory(ConfigurationManager.GetSection("quartz") as NameValueCollection);
            scheduler = schedulerFactory.GetScheduler();

            scheduler.Start();
        }

        public void Stop()
        {
            scheduler.Shutdown();

            log.InfoFormat("Shutting the agent down");
        }
    }
}
