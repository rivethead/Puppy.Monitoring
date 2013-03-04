using System;
using System.IO;
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
                if (File.Exists(requestFileName))
                    log.InfoFormat("Appending to {0}", requestFileName);

                if (File.Exists(responseFileName))
                    log.InfoFormat("Appending to {0}", responseFileName);
            }

            WriteContentToFile(requestFileName, request);
            WriteContentToFile(responseFileName, response);
        }

        private void WriteContentToFile(string filepath, string content)
        {
            log.InfoFormat("Writing track file to {0}", filepath);

            using (var file = new StreamWriter(filepath, true))
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