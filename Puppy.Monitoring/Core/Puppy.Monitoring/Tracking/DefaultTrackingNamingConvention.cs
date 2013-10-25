using System;
using System.IO;

namespace Puppy.Monitoring.Tracking
{
    public class DefaultTrackingNamingConvention : IDefineTrackingNamingConvention
    {
        private const string request = "REQUEST_";
        private const string response = "RESPONSE_";
        private const string fileExtention = "xml";
        private readonly string uniqueIdentifier;

        public DefaultTrackingNamingConvention()
        {
            uniqueIdentifier = DateTime.UtcNow.Ticks.ToString();
        }

        public string RequestFileName(string identifier, string uniqueIdentifier)
        {
            return string.Format("{0}_{1}_{2}.{3}",
                request,
                identifier,
                uniqueIdentifier,
                fileExtention).Trim(Path.GetInvalidFileNameChars());
        }

        public string ResponseFileName(string identifier, string uniqueIdentifier)
        {
            return string.Format("{0}_{1}_{2}.{3}",
                response,
                identifier,
                DateTime.UtcNow.Ticks,
                fileExtention).Trim(Path.GetInvalidFileNameChars());
        }

        public string RequestFileName(string identifier)
        {
            return RequestFileName(identifier, uniqueIdentifier);
        }

        public string ResponseFileName(string identifier)
        {
            return ResponseFileName(identifier, uniqueIdentifier);
        }
    }

    public class NamedTrackingNamingConvention : IDefineTrackingNamingConvention
    {
        private readonly string request;
        private readonly string response;
        private const string fileExtention = "xml";
        private readonly string uniqueIdentifier;

        public NamedTrackingNamingConvention(string request, string response)
        {
            this.request = request == "" ? "REQUEST_" : request;
            this.response = response == "" ? "RESPONSE_" : response;
            uniqueIdentifier = DateTime.UtcNow.Ticks.ToString();
        }

        public string RequestFileName(string identifier, string uniqueIdentifier)
        {
            return string.Format("{0}_{1}_{2}.{3}",
                request,
                identifier,
                uniqueIdentifier,
                fileExtention).Trim(Path.GetInvalidFileNameChars());
        }

        public string ResponseFileName(string identifier, string uniqueIdentifier)
        {
            return string.Format("{0}_{1}_{2}.{3}",
                response,
                identifier,
                DateTime.UtcNow.Ticks,
                fileExtention).Trim(Path.GetInvalidFileNameChars());
        }

        public string RequestFileName(string identifier)
        {
            return RequestFileName(identifier, uniqueIdentifier);
        }

        public string ResponseFileName(string identifier)
        {
            return ResponseFileName(identifier, uniqueIdentifier);
        }
    }
}