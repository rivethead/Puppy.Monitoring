using System;
using System.Collections.Generic;
using System.Linq;
using Puppy.Monitoring.Tracking;

namespace Puppy.Monitoring
{
    public class Track<TResponse>
    {
        private readonly Func<TResponse> call;
        private List<Func<ReportInfoCollector>> failure = new List<Func<ReportInfoCollector>>();
        private List<Func<ReportInfoCollector>> success = new List<Func<ReportInfoCollector>>();
        private Func<string> identifier;
        private string request;
        private Func<TResponse, string> serialise;
        private IWriteTracking writer;

        private Track(Func<TResponse> call)
        {
            this.call = call;
        }

        public Track<TResponse> Write(IWriteTracking @using, Func<string> identifier, string request, Func<TResponse, string> response)
        {
            this.identifier = identifier;
            writer = @using;
            this.request = request;
            serialise = response;

            return this;
        }

        public Track<TResponse> Report(Func<ReportInfoCollector> success, Func<ReportInfoCollector> failure)
        {
            this.success.Add(success);
            this.failure.Add(failure);
            return this;
        }

        public static Track<TResponse> Call(Func<TResponse> call)
        {
            return new Track<TResponse>(call);
        }

        private TResponse WrappedCall()
        {
            var callResponse = call();
            writer.Write(identifier(), request, serialise(callResponse));

            return callResponse;
        }

        public TResponse Go()
        {
            return Measure
                .This<TResponse>(WrappedCall)
                .OnSuccess(success)
                .OnFailure(failure)
                .Gauge();
        }

        public void Testing()
        {
            Track<string>
                .Call(() => string.Empty)
                .Write(null, () => string.Empty, string.Empty, s => string.Empty)
                .Go();
        }
    }
}