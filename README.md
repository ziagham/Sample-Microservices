# Sample Microservices

This program is a sample microservices that implemented via different patterns and architectures. In principle, this program consists of two RESTful services. These two services are completely independent and have its own database. One service for Customers and one service for Orders. Each service survives separately, but when a customer data is updated, then the order information for that customer will also be updated using the event-driven design. 

## Overview

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

- **Testing tools**  
    xUnit 2.4.1  
    FakeItEasy 7.1.0  
    FluentAssertions 5.10.3  