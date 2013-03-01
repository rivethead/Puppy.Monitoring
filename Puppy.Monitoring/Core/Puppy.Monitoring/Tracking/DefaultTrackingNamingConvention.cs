namespace Puppy.Monitoring.Tracking
{

	public class DefaultTrackingNamingConvention : IDefineTrackingNamingConvention
	{
		public string RequestPrefix 
		{
			get 
			{
				return "REQUEST_";
			}
		}

		public string ResponsePrefix 
		{
			get 
			{
				return "RESPONSE_";
			}
		}

	}
	
}