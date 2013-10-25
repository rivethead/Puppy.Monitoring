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
    public class when_publish_an_event_to_the_sql_server_publisher : Specification
    {
        private readonly SampleCustomEvent @event;
        private readonly SqlServerPublisher publisher;

        public when_publish_an_event_to_the_sql_server_publisher()
        {
            given_the_puppy_monitoring_database_is_empty();

            publisher = new SqlServerPublisher();
            @event = new SampleCustomEvent(new DateTime(2013, 01, 01, 12, 13, 14),
                                           new Categorisation("CATEGORY", "SUB_CATEGORY"),
                                           new Timings(100));
        }

        private void given_the_puppy_monitoring_database_is_empty()
        {
            using (
                var connection =
                    new SqlConnection(ConfigurationManager.ConnectionStrings["puppy.sqlserver"].ConnectionString))
            {
                connection.Open();

                connection.Execute("DELETE FROM ReportingEvent");
            }
        }

        public override void Observe()
        {
            publisher.Publish(@event);
        }

        public void the_event_is_published_to_the_database()
        {
            using (
                var connection =
                    new SqlConnection(ConfigurationManager.ConnectionStrings["puppy.sqlserver"].ConnectionString))
            {
                connection.Open();

                var events = connection.Query("SELECT TOP 1 * FROM ReportingEvent").ToList();

                events.Count().ShouldEqual(1);
                ObjectAssertExtensions.ShouldEqual(events[0].Category, "CATEGORY");
                ObjectAssertExtensions.ShouldEqual(events[0].SubCategory, "SUB_CATEGORY");
                ObjectAssertExtensions.ShouldEqual(events[0].PublishedOn, new DateTime(2013, 01, 01, 12, 13, 14));
            }
        }
    }
}