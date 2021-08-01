# Sample Microservices

This program is a sample microservices that implemented via different patterns and architectures. In principle, this program consists of two RESTful services. These two services are completely independent and have its own database. One service for Customers and one service for Orders. Each service survives separately, but when a customer data is updated, then the order information for that customer will also be updated using the event-driven design. 

## Overview

Both services can use SQL-Serverql or InMemoryDatabase, and this is determined by the settings in each service. Also in the settings, there are properties related to the event-bus that must be specified. It should be noted that event-bus properties must be configured for both service as same.

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
    MediatR (as Mediator Pattern)  
    Event-Driven Design (EDD) {RabbitMq and Azure ServiceBus}  

- **Databses**  
    InMemoryDatabase  
    SqlServer  

- **IDL (Interface description language)**  
    Swagger 5.6.3  

- **Other tools**  
    FluentValidation  
    Docker  

- **Testing tools**  
    xUnit 2.4.1  
    FakeItEasy 7.1.0  
    FluentAssertions 5.10.3  


### Run the Program

A few changes must be made before running. 

> Since this program is developed in the Linux environment, it is necessary to change the paths for the InputDirectory and OutputDirectory, as well as the path of the `ReferenceData` file in the **appsettings.json** file in **MainAppliction** project. 

Below you can see the program settings, which is in Json format. This file has more settings items. Such as **WatchingFilters** that can be used for watching on certain file extensions. **OutputResultSuffix** which adds the desired extension to the end of the output files. **OverwriteFile** used in a situation if the the output file name is exist in output directory, then it should be overwriten or a generate  a unique file name. **CheckDirectoryExistand** whether the existence of input and output directories are checked.

    {
      "AppSettings": {
          "InputDirectory": "/home/ziagham/...",
          "OutputDirectory": "/home/ziagham/...",
          "ReferenceDataPath": "/home/ziagham/.../ReferenceData.xml",
          "WatchingFilters": "*.*",
          "OutputResultSuffix": "-Result",
          "OverwriteFile": false,
          "CheckDirectoryExist": true
      }
	}
    
To run this program, first go to the main solution directory and then run the below commands in the **Terminal**, **Powershell** or **CMD**.

    dotnet restore
    dotnet build
    cd Src/MainApplication
    dotnet run