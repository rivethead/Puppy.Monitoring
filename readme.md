# Skinny.Monitoring #

A monitoring library to monitor

- Successes / failures in the application
- Duration of external system calls
- Requests and responses between external system calls

The library is split into three parts

- Reporting
- Measuring
- Tracking

## Reporting ##

The idea behind the reporting is to allow a developer to record events during the execution of the application. The simplest two events is the `SuccessEvent` and `FailureEvent`, but a developer can add their own event implementations by deriving a new event implementation from the Event class.

The report interface give you three options, report a success event, a failure event or a custom event.

    Report 
  		.Success() // success! 
		.Publish(); // publish the event using the parameters supplied 

	Report
		.Failure() // failure
		.Publish(); // publish the event

	Report
		.CustomEvent()
		.Event(new MyCustomEvent()) // MyCustomEvent implements IEvent
		.Publish();


## Measurement ##

The measurement part of the library is put in place to measure the time a call takes to execute and report based on the outcome of the call. Normally this is used when calling external services (e.g. web service, an api) but you can measure any call you fancy. The measurement functionality will set the `Timings.Took` value on the published event to the time (in milliseconds) the call took to complete.

    Measure
		.This<TResponse>(() => {
								// execute call to be measured and return an instance of TResponse
								})
		.OnSuccess(() => Report.Success()) // if the call completes a success event is published
		.OnFailure(() => Report.Failure()) // if the fails a failure event is published
		.Gauge() // do it!

## Tracking ##

The tracking element of the library builds on the Reporting and Measurement functionality to track communication between the local application and an external service. The Tracking functionality basically takes a call, the request used in the call and the response of the call and writes the request and response to a persistent data store. Once the call completed (or not) an event is published.

    Track<ExternalCallResponse>
    	.Call(() => call_to_external_service()) // the call to an external service
    .Write()
        .With(tracking_writer) // write the request and response to a persistent store using this implementation of IWriteTracking
        .UsingAsIdentifier(() => identifier) // what is the unique identifier for the call
        .TheRequest(request) // the request sent to the external service
        .TheResponse(response => response.ToString()) // the serialised response from the external service
    .Report()
        .Success(Report.Success) // if the call completes publish this event
		.Failure(Report.Failure) // if the call fails publish this event
    .Go();



    
