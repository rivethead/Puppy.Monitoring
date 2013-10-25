using System.Data.SqlClient;
using Puppy.Monitoring.Events;
using Puppy.Monitoring.SqlServer.Imps.Dapper.NET;

namespace Puppy.Monitoring.SqlServer.Imps
{
    public class PublishedEventMarker : IMarkPublishedEvents
    {
        private readonly SqlConnection connection;

        public PublishedEventMarker(SqlConnection connection)
        {
            this.connection = connection;
        }

        public void Mark(IEvent @event)
        {
            connection.Execute("UPDATE reportingEvent SET Republished = 1 WHERE Id = @id", new { id = @event.Id });
        }
    }
}