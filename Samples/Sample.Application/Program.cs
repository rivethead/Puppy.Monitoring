using System;
using System.Collections.Generic;
using Skinny.Monitoring;
using Skinny.Monitoring.ManualAdapter;
using Skinny.Monitoring.Pipeline;
using Skinny.Monitoring.Pipeline.Pipelets;
using Skinny.Monitoring.Pipeline.Pipelets.Counting;
using Skinny.Monitoring.Pipeline.Pipelets.Filters;
using Skinny.Monitoring.Pipeline.Pipelets.Notification;
using Skinny.Monitoring.Publishing;

namespace Sample.Application
{
    internal class Program
    {
        private static void Main(string[] args)
        {



            Console.WriteLine("s - Success");
            Console.WriteLine("f - Failure");
            Console.WriteLine("q - Quit");

            var adapter = CreateAdapter();
            Publisher.Use(adapter);

            Report.Success().InCategory("CATEGORY").InSubCategory("SUB_CATEGORY").Publish();

            Console.ReadLine();
        }

        private static IPipelineAdapter CreateAdapter()
        {
            var adapter = new ManualPipelineAdapter();
            adapter.Register(new QueuedEventsPipeline(new List<IPipelet>()
                {
                    new EventCountingPipelet(new InMemoryCounter()),
                    new SpecificEventCountingPipelet(new InMemoryCounter(), new SuccessEventSpecification()),
                    new SpecificEventCountingPipelet(new InMemoryCounter(), new FailureEventSpecification()),
                    new NotificationPipelet(new ConsoleNotifier(), new NumberOfEventsGreaterOrEqualThanSpecification(10)),
                    new NotificationPipelet(new AchievementUnlockedNotifier("Ninja"), new NumberOfEventsGreaterOrEqualThanSpecification(20)),
                }));
            return adapter;
        }
    }
}