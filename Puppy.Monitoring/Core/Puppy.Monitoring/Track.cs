using System;
using Puppy.Monitoring.Tracking;
using Puppy.Monitoring.Events;

namespace Puppy.Monitoring
{
	public class Track<TResponse>
	{
		string request;
		string response;
		IWriteTracking writer;
		Func<TResponse> call;
		Func<ReportInfoCollector> failure;
		Func<TResponse, string> serialise;
		Func<string> identifier;
		Func<ReportInfoCollector> success;
		
		private Track (Func<TResponse> call)
		{
			this.call = call;
		}

		public Track<TResponse> UsingRequest (string request)
		{
			this.request = request;
			return this;
		}

		public Track<TResponse> WriteUsing (IWriteTracking writer)
		{
			this.writer = writer;
			return this;
		}

		public static Track<TResponse> Call (Func<TResponse> call)
		{
			return new Track<TResponse> (call);
		}

		public Track<TResponse> SerialiseResponse (Func<TResponse, string> serialise)
		{
			this.serialise = serialise;
			return this;
		}

		public Track<TResponse> WithIdentifier (Func<string> identifier)
		{
			this.identifier = identifier;
			return this;
		}

		public Track<TResponse> OnSuccess (Func<ReportInfoCollector> success)
		{
			this.success = success;
			return this;
		}

		public Track<TResponse> OnFailure (Func<ReportInfoCollector> failure)
		{
			this.failure = failure;
			return this;
		}
		
		TResponse WrappedCall ()
		{
			var callResponse = call ();
			writer.Write (identifier (), request, serialise (callResponse));

			return callResponse;
		}
		
		public void Go ()
		{
			Measure
				.This<TResponse> (WrappedCall)
				.OnSuccess (() => success)
				.OnFailure (() => failure)
				.Gauge ();
		}

		public void Testing ()
		{
			Track<string>
				.Call (() => string.Empty)
				.UsingRequest (new object ().ToString ())
				.SerialiseResponse (r => string.Empty)
				.WriteUsing (new FileTrackingWriter ())
				.WithIdentifier (() => string.Empty)
					.OnSuccess (() => new SuccessEvent (new Categorisation("TEST")))
					.OnFailure (() => new FailureEvent (new Categorisation("TEST")))
				.Go ();
		}
	}





}