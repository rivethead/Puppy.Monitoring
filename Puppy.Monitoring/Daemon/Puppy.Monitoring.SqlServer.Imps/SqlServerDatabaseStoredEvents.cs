﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using Common.Logging;
using Puppy.Monitoring.Events;
using Puppy.Monitoring.Publishing;
using Puppy.Monitoring.SqlServer.Imps.Dapper.NET;

namespace Puppy.Monitoring.SqlServer.Imps
{
    public class SqlServerDatabaseStoredEvents : IProviderStoredEvents
    {
        private SqlConnection connection;
        private static readonly ILog log = LogManager.GetLogger<SqlServerDatabaseStoredEvents>();

        public SqlServerDatabaseStoredEvents(SqlConnection connection)
        {
            this.connection = connection;
        }


        public SqlServerDatabaseStoredEvents()
        {
        }

        public IList<IEvent> GetEvents()
        {
            using (var cnn = EnsureConnection())
            {
                return GetLatestEvents(cnn);
            }
        }

        private List<IEvent> GetLatestEvents(SqlConnection cnn)
        {
            var eventData = cnn.Query("SELECT * FROM ReportingEvent");
            var events = new List<IEvent>();

            foreach (var @event in eventData)
            {
                log.InfoFormat("Trying to rebuild and republish event of type {0}, {1}", @event.FullEventType, @event.EventAssembly);

                try
                {
                    var eventType = Type.GetType(string.Format("{0}, {1}", @event.FullEventType, @event.EventAssembly));
                    var rebuiltType = Activator.CreateInstance(eventType, new object[]
                        {
                            new PublishingContext(@event.System, @event.Module, @event.MachineName, string.Empty),
                            new EventTiming(@event.PublishedOn),
                            new Categorisation(@event.Category, @event.SubCategory, @event.Segment),
                            @event.CorrelationId,
                            new Timings(@event.TookMilliseconds),
                            @event.Id
                        }) as IEvent;

                    events.Add(rebuiltType);
                }
                catch (Exception e)
                {
                    log.InfoFormat("Failed to rebuild and republish event of type {0}, {1}", @event.FullEventType, @event.EventAssembly);
                    log.ErrorFormat("The failure was {0}", e);
                }
            }

            return events;
        }


        private SqlConnection EnsureConnection()
        {
            if (connection != null)
            {
                connection.Open();
                return connection;
            }

            var connectionString = ConfigurationManager.ConnectionStrings["puppy.sqlserver"];

            if (connectionString == null)
                throw new ConfigurationErrorsException("Failed to find connection called 'puppy.sqlserver'");

            if (string.IsNullOrEmpty(connectionString.ConnectionString))
                throw new ConfigurationErrorsException("Failed to find connection called 'puppy.sqlserver'");


            connection = new SqlConnection(connectionString.ConnectionString);
            connection.Open();

            return connection;
        }
    }
}