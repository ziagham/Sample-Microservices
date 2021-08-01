# Sample Microservices

This program is a sample microservices that implemented via different patterns and architectures. In principle, this program consists of two RESTful services. These two services are completely independent and have its own database. One service for Customers and one service for Orders. Each service survives separately, but when a customer data is updated, then the order information for that customer will also be updated using the event-driven design. 

## Overview

Both services can use `SQL-Server` or `InMemoryDatabase`, and this is determined by the settings in each service. Also in the settings, there are properties related to the event-bus that must be specified. It should be noted that event-bus properties must be configured for both service as same.

### Implementation specifications
This program is implemented in C# programming language and uses .NET 5 technology.
Below you will find a complete list of technologies, tools, environments, methodologies, and patterns used to implement this program.

- **Languages and Frameworks**  
    C# 9.0  
    .NET 5 (.NET core)  
    LINQ  

- **Operating System**  
    Linux Operating System (Ubuntu 18.04 LTS)  

- **IDE (Integrated Development Environment)**  
    VS Code 1.58.2

- **Paradigms**  
    Object-Oriented Programming (OOP)  
    Dependency Injection (DI)  

- **Patterns and Architectures**  
    Repository Pattern  
    MediatR (as Mediator Pattern) 9.0.0  
    Event-Driven Design (EDD) {RabbitMq and Azure ServiceBus}  

- **Databses**  
    InMemoryDatabase  
    SqlServer  

- **IDL (Interface description language)**  
    Swagger 5.6.3  

- **Other tools**  
    FluentValidation 10.3.0  
    Docker  

- **Testing tools**  
    xUnit 2.4.1  
    FakeItEasy 7.1.0  
    FluentAssertions 5.10.3  


### Run the Program

A few changes must be made before running. 

> There is an important property in the appsettings.json file, **BaseServiceSettings**, that must be specified before running. This property have two sub properties `UseInMemoryDatabase` and `UserabbitMq`, which specify the type of database to be used, and whether to use UserabbitMq or AzureServiceBus respectively. 

By default, InMemoryDatabase and AzureServiceBus are used. An AzureServiceBus `ConnectionString` is also set to make running the program with less configurations. Also, if you want to use RabbitMq, just set the UserabbitMq property to true. Additionally, if you need to use `SQL-Server`, you need to configure ConnectionStrings to your database.

Below you can see the program settings, which is in Json format.

    {
        "RabbitMq": {
            "Hostname": "rabbitmq",
            "QueueName": "CustomerQueue",
            "UserName": "guest",
            "Password": "guest"
        },
        "ConnectionStrings": {
            "CustomerDatabase": ""
        },
        "BaseServiceSettings": {
            "UseInMemoryDatabase": true,
            "UserabbitMq": false
        },
        "AzureServiceBus": {
            "ConnectionString": "",
            "QueueName": "CustomerQueue"
        }
	}
    
This program can also be run as a `docker`. There are two `docker-compose` files in the main root of this repository. If the service images have not been created before, you can use the `docker-compose.build.yml` file. By run this file, service images are created and then executed. On the other hand, if the service images have already been created, the `docker-compose.ymld file can be used to run.

To run this program, first go to the main root of this repository and then run the below commands in the **Terminal**, **Powershell** or **CMD**.

    docker-compose -f docker-compose.build.yml up