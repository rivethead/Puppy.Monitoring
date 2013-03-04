using System;
using System.IO;

namespace Puppy.Monitoring.Tracking
{
    public class DefaultTrackingNamingConvention : IDefineTrackingNamingConvention
    {
        private const string request = "REQUEST_";
        private const string response = "RESPONSE_";
        private const string fileExtention = "xml";

        public string RequestFileName(string identifier)
        {
            return string.Format("{0}_{1}_{2}.{3}", 
                request, 
                identifier, 
                DateTime.UtcNow.Ticks,
                fileExtention).Trim(Path.GetInvalidFileNameChars());
        }

        public string ResponseFileName(string identifier)
        {
            return string.Format("{0}_{1}_{2}.{3}", 
                response, 
                identifier, 
                DateTime.UtcNow.Ticks,
                fileExtention).Trim(Path.GetInvalidFileNameChars());
        }
    }
}