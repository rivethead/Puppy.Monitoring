using System.IO;
using System.Text.RegularExpressions;
using Puppy.Monitoring.Tracking;

namespace Puppy.Monitoring.Contrib.Tracking
{
    public class SouthAfricanIdNumberDistribution : IFileDistributionAlgorithm
    {
        private readonly SouthAfricanIdNumber idNumber;
        private readonly string baseFolder;

        public SouthAfricanIdNumberDistribution(string idNumber, string baseFolder)
        {
            this.idNumber = new SouthAfricanIdNumber(idNumber);
            this.baseFolder = baseFolder;
        }
        public string GetFileLocation(string filename)
        {
            return Path.Combine(baseFolder, idNumber.Checksum, idNumber.BirthMonth, idNumber.BirthDay, idNumber.IDNumber, filename);
        }
    }

    internal class SouthAfricanIdNumber
    {
        public SouthAfricanIdNumber(string idNumber)
        {
            IDNumber = idNumber;
        }

        public string IDNumber { get; private set; }

        public string Checksum
        {
            get { return IDNumber.Substring(IDNumber.Length - 1); }
        }

        public string BirthMonth
        {
            get { return IDNumber.Substring(2, 2); }
        }

        public string BirthDay
        {
            get { return IDNumber.Substring(4, 2); }
        }

        public bool IsValidIDNumber()
        {
            if (string.IsNullOrEmpty(IDNumber))
                return false;

            const string pattern = @"^(\d{13})?$";

            return Regex.IsMatch(IDNumber, pattern);
        }
    }
}