using System;
using System.Data;
using Puppy.Monitoring.Events;
using Puppy.Monitoring.Publishing;
using Puppy.Monitoring.SqlServerPublisher.Dapper.NET;

namespace Puppy.Monitoring.SqlServerPublisher
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
                                Category, SubCategory, Segment,
                                TookMilliseconds, EventType, System, Module, MachineName, CorrelationId
                        )
                        VALUES(
                            @id, 
                                @publishedOn, @year, @month, @day, @hour, @minute, @second, @timestamp,
                                @category, @subCategory, @segment,
                                @milliseconds, @eventType, @system, @module, @machineName, @correlationId
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
                            segment = @event.Categorisation.Segmentation,
                            milliseconds = @event.Timings.Took, 
                            eventType = @event.GetType().Name,
                            system = @event.Context.System,
                            module = @event.Context.Module,
                            machineName = @event.Context.MachineName,
                            correlationId = @event.CorrelationId
                        }
                };
            connection.Execute(sql, parameters);
        }
    }
}