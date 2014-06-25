using System;
using System.Data;
using Microsoft.SqlServer.Server;
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
                                TookMilliseconds, EventType, System, Module, MachineName, CorrelationId, FullEventType, EventAssembly, Republished,
                                [Parameters], [Description]
                        )
                        VALUES(
                            @id, 
                                @publishedOn, @year, @month, @day, @hour, @minute, @second, @timestamp,
                                @category, @subCategory, @segment,
                                @milliseconds, @eventType, @system, @module, @machineName, @correlationId, @fullEventType, @eventAssembly, @republished,
                                @parameters, @description
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
                            fullEventType = @event.GetType().FullName, 
                            eventAssembly = @event.GetType().Assembly.GetName().Name,
                            system = @event.Context.System,
                            module = @event.Context.Module,
                            machineName = @event.Context.MachineName,
                            correlationId = @event.CorrelationId,
                            republished = false,
                            parameters = @event.Description != null ? @event.Description.Parameters : string.Empty,
                            description = @event.Description != null ? @event.Description.Description : string.Empty
                        }
                };
            connection.Execute(sql, parameters);
        }
    }
}