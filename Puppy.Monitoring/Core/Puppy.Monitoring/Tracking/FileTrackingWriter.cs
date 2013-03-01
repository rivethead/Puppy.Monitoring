namespace Puppy.Monitoring.Tracking
{
	public class FileTrackingWriter : IWriteTracking
	{
		IDefineTrackingNamingConvention namingConvention;

		public FileTrackingWriter () : this(new DefaultTrackingNamingConvention())
		{
		}
		

		public FileTrackingWriter(IDefineTrackingNamingConvention namingConvention)
		{
			this.namingConvention = namingConvention;
		}

		public void Write (string identifier, string request, string response)
		{
		}

		public void Dispose ()
		{
		}
	}
}