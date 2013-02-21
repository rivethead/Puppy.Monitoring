using System;
using System.Diagnostics;

namespace Puppy.Monitoring
{
    public class Measure
    {
        public static MeasurementInfoCollector<TReturn> This<TReturn>(Func<TReturn> action)
        {
            return new MeasurementInfoCollector<TReturn>(action);
        }
    }

    public class MeasurementInfoCollector<TReturn>
    {
        private readonly Func<TReturn> actionToMeasure = () => default(TReturn);
        private Func<ReportInfoCollector> failure;
        private Func<ReportInfoCollector> success;

        public MeasurementInfoCollector(Func<TReturn> actionToMeasure)
        {
            this.actionToMeasure = actionToMeasure;
        }

        public MeasurementInfoCollector<TReturn> OnFailure(Func<ReportInfoCollector> failure)
        {
            this.failure = failure;
            return this;
        }

        public MeasurementInfoCollector<TReturn> OnSuccess(Func<ReportInfoCollector> success)
        {
            this.success = success;
            return this;
        }

        public TReturn Gauge()
        {
            var stopwatch = new Stopwatch();
            TReturn result = default(TReturn);
            try
            {
                stopwatch.Start();

                result = actionToMeasure();

                stopwatch.Stop();

                Execute(success, stopwatch);
            }
            catch
            {
                stopwatch.Stop();
                Execute(failure, stopwatch);
                throw;
            }

            return result;
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