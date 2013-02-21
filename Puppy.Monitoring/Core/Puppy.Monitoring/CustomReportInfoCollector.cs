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

        public CustomReportInfoCollector Event(IEvent @event)
        {
            this.@event = @event;
            return this;
        }

        public void Publish()
        {
            report.Publish(new ReportingEventEchoBuilder(@event));
        }
    }
}