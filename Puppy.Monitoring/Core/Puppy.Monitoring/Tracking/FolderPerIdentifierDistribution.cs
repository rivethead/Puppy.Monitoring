using System.IO;

namespace Puppy.Monitoring.Tracking
{
    public class FolderPerIdentifierDistribution : IFileDistributionAlgorithm
    {
        private readonly string baseFolder;
        private readonly string identifier;

        public FolderPerIdentifierDistribution(string baseFolder, string identifier)
        {
            this.baseFolder = baseFolder;
            this.identifier = identifier;
        }

        public string GetFileLocation(string filename)
        {
            return Path.Combine(baseFolder, identifier, filename.Trim(Path.GetInvalidPathChars()).Trim(Path.GetInvalidFileNameChars()));
        }
    }
}