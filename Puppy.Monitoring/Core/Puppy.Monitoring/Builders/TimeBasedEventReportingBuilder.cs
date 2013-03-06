using Puppy.Monitoring.Events;

namespace Puppy.Monitoring.Builders
{
    internal class TimeBasedEventReportingBuilder : IBuildReportingEvent
    {
        private readonly ReportInfoCollector info;

        public TimeBasedEventReportingBuilder(ReportInfoCollector info)
        {
            this.info = info;
        }

        public IEvent Build()
        {
            return info.Success
                       ? (IEvent) new SuccessEvent(new Categorisation(info.Category, info.SubCategory, info.Segment),
                                                   new Timings(info.Milliseconds), info.CorrelationId)
                       : new FailureEvent(new Categorisation(info.Category, info.SubCategory),
                                          new Timings(info.Milliseconds), info.CorrelationId);
        }
    }
}