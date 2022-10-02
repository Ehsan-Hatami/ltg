# Live Tix Group Technical test

## Task

Create a Web API that when called:
* Calls, combines and returns the results of:
    * http://jsonplaceholder.typicode.com/photos
    * http://jsonplaceholder.typicode.com/albums
* Allows an integrator to filter on the user id – so just returns the albums and photos relevant to a single user.

## High level design
* The solution is build based on .netcore 6 to benefits from the latest feature of the programming language.
* Swagger framework is implemented to help the visualisation and execution of the provided endpoints
* IOC is introduced to hanlde life cycle of the services
    * The services are defined as transient which there will be one new instance per request
    * Service registration extension is defined to help clean code
* Http calls are isolated into their own class to tackle seperation of concern
* All the services are covered by interface to support injection and mocking for unitTests
* AutoMapper library is used to convert and map the third party api responses to inner domain models
* Each funciton decorated by a summary tag to support documentation
* Aggregation is smart enough to apply userId filter appropatley to help the performance
* AutoMocker is used to mock interfaces to support unitTest
* AutoFixture is used to create random data for stub entities
* unitTest coverage has reached 100% by excluding 
    * Controller 
        * Not much logic was involved in controller, hence no point on unitTesting the controller
        * if controller gets extended (include validation for example) then unitTest can be added
    * Program class
    * startup class

## Room for improvments
* The non-filtered results currently takes some time to load up due to high volum of the data. Paginaiton can help performance improvments
* Caching the result also can help performance
* Retry policy (Polly framework) on api call failure can make the application robust