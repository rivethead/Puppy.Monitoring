using System;
using System.Diagnostics;

namespace Puppy.Monitoring
{
    public class MeasurementInfoCollector
    {
        private readonly Action actionToMeasure = () => { };
        private Func<ReportInfoCollector> failure;
        private Func<ReportInfoCollector> success;

        public MeasurementInfoCollector(Action actionToMeasure)
        {
            this.actionToMeasure = actionToMeasure;
        }

        public MeasurementInfoCollector OnFailure(Func<ReportInfoCollector> failure)
        {
            this.failure = failure;
            return this;
        }

        public MeasurementInfoCollector OnSuccess(Func<ReportInfoCollector> success)
        {
            this.success = success;
            return this;
        }

        public void Gauge()
        {
            var stopwatch = new Stopwatch();
            try
            {
                stopwatch.Start();

                actionToMeasure();

                stopwatch.Stop();

                Execute(success, stopwatch);
            }
            catch
            {
                stopwatch.Stop();
                Execute(failure, stopwatch);
                throw;
            }
        }

        private void Execute(Func<ReportInfoCollector> report, Stopwatch stopwatch)
        {
            if (report == null)
                return;

            report()
                .ItTook(stopwatch.ElapsedMilliseconds)
                .Publish();
        }
    }
}