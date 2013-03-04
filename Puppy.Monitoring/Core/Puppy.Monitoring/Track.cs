using System;
using Puppy.Monitoring.Tracking;

namespace Puppy.Monitoring
{
    public class Track<TResponse>
    {
        private readonly Func<TResponse> call;
        private Func<ReportInfoCollector> failure;
        private Func<string> identifier;
        private string request;
        private Func<TResponse, string> serialise;
        private Func<ReportInfoCollector> success;
        private IWriteTracking writer;

        private Track(Func<TResponse> call)
        {
            this.call = call;
        }

        public Track<TResponse> UsingRequest(string request)
        {
            this.request = request;
            return this;
        }

        public Track<TResponse> WriteUsing(IWriteTracking writer)
        {
            this.writer = writer;
            return this;
        }

        public static Track<TResponse> Call(Func<TResponse> call)
        {
            return new Track<TResponse>(call);
        }

        public Track<TResponse> SerialiseResponse(Func<TResponse, string> serialise)
        {
            this.serialise = serialise;
            return this;
        }

        public Track<TResponse> WithIdentifier(Func<string> identifier)
        {
            this.identifier = identifier;
            return this;
        }

        public Track<TResponse> OnSuccess(Func<ReportInfoCollector> success)
        {
            this.success = success;
            return this;
        }

        public Track<TResponse> OnFailure(Func<ReportInfoCollector> failure)
        {
            this.failure = failure;
            return this;
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
                .OnSuccess(() => success())
                .OnFailure(() => failure())
                .Gauge();
        }

        public void Testing()
        {
            Track<string>
                .Call(() => string.Empty)
                .UsingRequest(new object().ToString())
                .SerialiseResponse(r => string.Empty)
                .WriteUsing(new FileTrackingWriter(null, null))
                .WithIdentifier(() => string.Empty)
                .OnSuccess(Report.Success)
                .OnFailure(Report.Failure)
                .Go();
        }
    }
}