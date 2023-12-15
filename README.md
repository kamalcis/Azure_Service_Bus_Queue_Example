# Azure_Service_Bus_Queue_Example

The example project add weather data to the azure service bus queue "add-weather-data"
The post method of the controller contains the necessary code.

An Interface IServiceBus has been created to define the signature of getting the service bus client
ServiceBus class implements the method of IServiceBus mehtod. 
IConfiguration object has been created through the ServiceBus constructor using the dependency injection feature of framework.
IConfiguraation reads the appsettings.json

IServiceBus and ServiceBus has been registered to the built in IoC container to enable dependency injection
WeatherForecastController class use the IServiceBus to create its object. 

