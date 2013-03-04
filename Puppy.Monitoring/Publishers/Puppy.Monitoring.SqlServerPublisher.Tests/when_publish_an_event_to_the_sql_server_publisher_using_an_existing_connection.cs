using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using Puppy.Monitoring.Events;
using Puppy.Monitoring.SqlServerPublisher.Dapper.NET;
using Puppy.Monitoring.SqlServerPublisher.Tests._helpers;
using Xunit.Extensions;

namespace Puppy.Monitoring.SqlServerPublisher.Tests
{
    public class when_publish_an_event_to_the_sql_server_publisher_using_an_existing_connection : Specification
    {
        private readonly SampleCustomEvent @event;
        private readonly SqlServerWithExistingConnectionPublisher publisher;
        private SqlConnection connection;

        public when_publish_an_event_to_the_sql_server_publisher_using_an_existing_connection()
        {
            given_the_puppy_monitoring_database_is_empty();

            connection = new SqlConnection(ConfigurationManager.ConnectionStrings["puppy.sqlserver"].ConnectionString);
            connection.Open();

            publisher = new SqlServerWithExistingConnectionPublisher(connection);
            @event = new SampleCustomEvent(new DateTime(2013, 01, 01, 12, 13, 14),
                                           new Categorisation("CATEGORY", "SUB_CATEGORY"),
                                           new Timings(100));
        }

        private void given_the_puppy_monitoring_database_is_empty()
        {
            using (
                var delete_connection =
                    new SqlConnection(ConfigurationManager.ConnectionStrings["puppy.sqlserver"].ConnectionString))
            {
                delete_connection.Open();

                delete_connection.Execute("DELETE FROM ReportingEvent");
            }
        }

        public override void Observe()
        {
            try
            {
                publisher.Publish(@event);
            }
            finally
            {
                if(connection != null)
                {
                    connection.Dispose();
                    connection = null;
                }
            }
        }

        [Observation]
        public void the_event_is_published_to_the_database()
        {
            using (var query_connection = new SqlConnection(ConfigurationManager.ConnectionStrings["puppy.sqlserver"].ConnectionString))
            {
                query_connection.Open();

                var events = query_connection.Query("SELECT TOP 1 * FROM ReportingEvent").ToList();

                events.Count().ShouldEqual(1);
                ObjectAssertExtensions.ShouldEqual(events[0].Category, "CATEGORY");
                ObjectAssertExtensions.ShouldEqual(events[0].SubCategory, "SUB_CATEGORY");
                ObjectAssertExtensions.ShouldEqual(events[0].PublishedOn, new DateTime(2013, 01, 01, 12, 13, 14));
            }
        }
    }
}