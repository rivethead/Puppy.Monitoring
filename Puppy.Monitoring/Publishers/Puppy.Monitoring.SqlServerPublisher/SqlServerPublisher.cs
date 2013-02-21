using System.Configuration;
using System.Data.SqlClient;
using Puppy.Monitoring.Events;
using Puppy.Monitoring.Publishing;

namespace Puppy.Monitoring.SqlServerPublisher
{
    public class SqlServerPublisher : IPublishEvents
    {
        public void Publish(IEvent @event)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["puppy.sqlserver"];

            if(connectionString == null)
                throw new ConfigurationErrorsException("Failed to find connection called 'puppy.sqlserver'");

            if(string.IsNullOrEmpty(connectionString.ConnectionString))
                throw new ConfigurationErrorsException("Failed to find connection called 'puppy.sqlserver'");

            using (var connection = new SqlConnection(connectionString.ConnectionString))
            {
                connection.Open();
                new SqlServerWithExistingConnectionPublisher(connection).Publish(@event);
            }
        }
    }
}