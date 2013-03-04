using System.IO;

namespace Puppy.Monitoring.Tracking
{
    public class SingleFolderFileDistribution : IFileDistributionAlgorithm
    {
        private readonly string baseFolder;
        private readonly string folderName;

        public SingleFolderFileDistribution(string folderName, string baseFolder)
        {
            this.folderName = folderName;
            this.baseFolder = baseFolder;
        }

        public string GetFileLocation(string filename)
        {
            return Path.Combine(baseFolder, folderName, filename);
        }
    }
}