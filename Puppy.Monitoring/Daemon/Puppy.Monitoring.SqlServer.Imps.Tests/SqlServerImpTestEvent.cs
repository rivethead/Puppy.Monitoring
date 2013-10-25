using System;
using Puppy.Monitoring.Events;
using Puppy.Monitoring.Publishing;

namespace Puppy.Monitoring.SqlServer.Imps.Tests
{
    public class SqlServerImpTestEvent : Event
    {
        public SqlServerImpTestEvent(Categorisation categorisation, Guid correlationId) : base(categorisation, correlationId)
        {
        }

        public SqlServerImpTestEvent(Categorisation categorisation, Timings timings, Guid correlationId) : base(categorisation, timings, correlationId)
        {
        }

        public SqlServerImpTestEvent(DateTime publishedOn, Categorisation categorisation, Timings timings) : base(publishedOn, categorisation, timings)
        {
        }

        public SqlServerImpTestEvent(PublishingContext context, EventTiming eventAudit, Categorisation categorisation, Guid correlationId, Timings timings, Guid id) : base(context, eventAudit, categorisation, correlationId, timings, id)
        {
        }

        public SqlServerImpTestEvent(DateTime publishedOn, Categorisation categorisation, Timings timings, Guid correlationId) : base(publishedOn, categorisation, timings, correlationId)
        {
        }
    }
}