namespace Puppy.Monitoring.Tracking
{
    public interface IDefineTrackingNamingConvention
	{
        string RequestFileName(string identifier);
        string ResponseFileName(string identifier);
	}

	
}