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



