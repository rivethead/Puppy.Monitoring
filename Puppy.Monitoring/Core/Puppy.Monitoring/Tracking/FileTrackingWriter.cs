using System.IO;

namespace Puppy.Monitoring.Tracking
{
    public class FileTrackingWriter : IWriteTracking
    {
        private readonly IFileDistributionAlgorithm distributionAlgorithm;
        private readonly IDefineTrackingNamingConvention namingConvention;

        public FileTrackingWriter(IFileDistributionAlgorithm distributionAlgorithm)
            : this(new DefaultTrackingNamingConvention(), distributionAlgorithm)
        {
            this.distributionAlgorithm = distributionAlgorithm;
        }

        public FileTrackingWriter(IDefineTrackingNamingConvention namingConvention, IFileDistributionAlgorithm distributionAlgorithm)
        {
            this.namingConvention = namingConvention;
            this.distributionAlgorithm = distributionAlgorithm;
        }

        public void Write(string identifier, string request, string response)
        {
            File.WriteAllText(distributionAlgorithm.GetFileLocation(namingConvention.RequestFileName(identifier)), request);
            File.WriteAllText(distributionAlgorithm.GetFileLocation(namingConvention.ResponseFileName(identifier)), request);
        }

        public void Dispose()
        {
        }
    }
}