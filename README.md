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
    AspNetCore.Versioning 5.0.0  
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

It is assumed that the docker is installed on your computer.

### Perform some operations

#### Customer Service

Customer service includes some REST methods. These methods include Customer list, new customer registration and customer update. The following commands can be used to call each method.

##### GET customers list
    curl --request GET --url http://localhost:5000/api/v1.0/customer

##### Register new customer
    curl --request POST --url http://localhost:5000/api/v1.0/customer --header 'content-type: application/json' -d '{ "FirstName":"Johnny", "LastName":"Depp", "Email":"johnny.depp@gmail.com", "BirthDate":"1963-06-09T18:25:43.511Z"}'

##### Update customer data
    curl --request PUT --url http://localhost:5000/api/v1.0/customer --header 'content-type: application/json' -d '{ "Id": "9f35b48d-cb87-4783-bfdb-21e36012930a", "FirstName":"Amin2", "LastName":"Ziagham2", "Email":"amin.ziagham@gmail.com", "BirthDate":"2012-04-23T18:25:43.511Z"}'

#### Order Service

On the other hand, Order service also have some REST methods. 

##### GET orders list (just payed orders not all)
    curl --request GET --url http://localhost:6000/api/v1.0/order

##### Register new order (Remember to register customer before order registration)
    curl --request POST --url 'http://localhost:6000/api/v1.0/order' --header 'content-type: application/json' -d '{ "CustomerGuid": "9264f09c-83f8-4bef-a00b-00d673bb274c", "CustomerFullName": "Johnny Depp"}'

##### Pay order
    curl --request PUT --url http://localhost:6000/api/v1.0/order/pay/c34a25d8-e786-4e00-9b70-6acf2e6187ac --header 'content-type: application/json' -d {}

