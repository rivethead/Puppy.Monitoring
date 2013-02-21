using System.Configuration;
using System.Data.SqlClient;
using Skinny.Monitoring.Events;
using Skinny.Monitoring.Publishing;

namespace Skinny.Monitoring.SqlServerPublisher
{
    public class SqlServerPublisher : IPublishEvents
    {
        public void Publish(IEvent @event)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["skinny.sqlserver"];

            if(connectionString == null)
                throw new ConfigurationErrorsException("Failed to find connection called 'skinny.sqlserver'");

            if(string.IsNullOrEmpty(connectionString.ConnectionString))
                throw new ConfigurationErrorsException("Failed to find connection called 'skinny.sqlserver'");

            using (var connection = new SqlConnection(connectionString.ConnectionString))
            {
                connection.Open();
                new SqlServerWithExistingConnectionPublisher(connection).Publish(@event);
            }
        }
    }
}