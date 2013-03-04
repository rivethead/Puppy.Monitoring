namespace Puppy.Monitoring.Tracking
{
    public interface IFileDistributionAlgorithm
    {
        string GetFileLocation(string filename);
    }
}