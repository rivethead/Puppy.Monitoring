using System;
using System.IO;
using System.Security.AccessControl;
using Common.Logging;

namespace Puppy.Monitoring.Tracking
{
    public class FileTrackingWriter : IWriteTracking
    {
        private readonly IFileDistributionAlgorithm distributionAlgorithm;
        private readonly IDefineTrackingNamingConvention namingConvention;
        private static readonly ILog log = LogManager.GetLogger<FileTrackingWriter>();

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

        public void Write(string identifier, string request, string response, bool overwrite = true)
        {
            var requestFileName = distributionAlgorithm.GetFileLocation(namingConvention.RequestFileName(identifier));
            var responseFileName = distributionAlgorithm.GetFileLocation(namingConvention.ResponseFileName(identifier));

            if (!overwrite)
            {
                if (File.Exists(requestFileName.FullPath))
                    log.InfoFormat("Appending to {0}", requestFileName);

                if (File.Exists(responseFileName.FullPath))
                    log.InfoFormat("Appending to {0}", responseFileName);
            }

            WriteContentToFile(requestFileName, request);
            WriteContentToFile(responseFileName, response);
        }

        private void WriteContentToFile(FileLocation fileLocation, string content)
        {
            log.InfoFormat("Writing track file to {0}", fileLocation);

            if (!Directory.Exists(fileLocation.Folder))
            {
                log.InfoFormat("Creating folder called {0} to store tracking file {1}", fileLocation.Folder, fileLocation.FileName);
                Directory.CreateDirectory(fileLocation.Folder);
            }

            using (var file = new StreamWriter(fileLocation.FullPath, true))
            {
                var bytes = new byte[content.Length * sizeof(char)];
                Buffer.BlockCopy(content.ToCharArray(), 0, bytes, 0, bytes.Length);
                file.Write(bytes);
            }

        }

        public void Dispose()
        {
        }
    }
}