using System;
using System.Data;
using Skinny.Monitoring.Events;
using Skinny.Monitoring.Publishing;
using Skinny.Monitoring.SqlServerPublisher.Dapper.NET;

namespace Skinny.Monitoring.SqlServerPublisher
{
    public class SqlServerWithExistingConnectionPublisher : IPublishEvents
    {
        private readonly IDbConnection connection;

        public SqlServerWithExistingConnectionPublisher(IDbConnection connection)
        {
            this.connection = connection;
        }

        public void Publish(IEvent @event)
        {
            const string sql = @"INSERT INTO ReportingEvent 
                        (
                            id, 
                                PublishedOn, Year, Month, Day, Hour, Minute, Second, Timestamp, 
                                Category, SubCategory, 
                                TookMilliseconds, EventType
                        )
                        VALUES(
                            @id, 
                                @publishedOn, @year, @month, @day, @hour, @minute, @second, @timestamp,
                                @category, @subCategory,
                                @milliseconds, @eventType
                )";

            var parameters = new[]
                {
                    new
                        {
                            id = Guid.NewGuid(),
                            publishedOn = @event.EventAudit.PublishedOn,
                            year = @event.EventAudit.Buckets.Year,
                            month = @event.EventAudit.Buckets.Month,
                            day = @event.EventAudit.Buckets.Day,
                            hour = @event.EventAudit.Buckets.Hour,
                            minute = @event.EventAudit.Buckets.Minute,
                            second = @event.EventAudit.Buckets.Second,
                            timestamp = @event.EventAudit.Buckets.UnixTimestamp,
                            category = @event.Categorisation.Category,
                            subCategory  = @event.Categorisation.SubCategory,
                            milliseconds = @event.Timings.Took, 
                            eventType = @event.GetType().Name
                        }
                };
            connection.Execute(sql, parameters);
        }
    }
}