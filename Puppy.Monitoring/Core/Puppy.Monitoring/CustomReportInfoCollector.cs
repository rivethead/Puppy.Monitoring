using Puppy.Monitoring.Builders;
using Puppy.Monitoring.Events;

namespace Puppy.Monitoring
{
    public class CustomReportInfoCollector
    {
        private readonly Report report;
        private IEvent @event;

        public CustomReportInfoCollector(Report report)
        {
            this.report = report;
        }

        public ReportInfoCollector Event(IEvent @event)
        {
            return new ReportInfoCollector(report, @event);
        }

        public void Publish()
        {
            report.Publish(new ReportingEventEchoBuilder(@event));
        }
    }
}