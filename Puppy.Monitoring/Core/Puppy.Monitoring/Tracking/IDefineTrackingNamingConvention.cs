namespace Puppy.Monitoring.Tracking
{
	public interface IDefineTrackingNamingConvention
	{
		string RequestPrefix { get; }

		string ResponsePrefix { get; }
	}

	
}