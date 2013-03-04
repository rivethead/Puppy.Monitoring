Skinny.Monitoring
=================

A monitoring library to monitor
* Successes / failures in the application
* Duration of external system calls
* Requests and responses between external system calls

The library is split into three parts
* Reporting
* Measuring
* Tracking

Reporting
---------
The idea behind the reporting is to allow a developer to record events during the execution of the application. The simplest two events is the `SuccessEvent` and `FailureEvent`, but a developer can add their own event implementations by deriving a new event implementation from the Event class.

To report a success event in the application use the fluent interface starting from the `Report` class.

``` C#
Report
  .Success() // success!
	.InCategory("CATEGORY_NAME") // the category to which the event belong
	.InSubCategory("SUB_CATEGORY") // the sub category of the category to which the event belong
	.SegmentedAs("SEGMENT") // the segment in the sub category to which the event belong
	.ItTook(1234) // how long the event took in milliseconds
	.Publish(); // publish the event using the parameters supplied
```



