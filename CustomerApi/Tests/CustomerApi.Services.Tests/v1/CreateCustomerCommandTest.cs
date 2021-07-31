// using System;
// using System.Threading;
// using System.Threading.Tasks;
// using Xunit;
// using MediatR;
// using Moq;
// using CustomerApi.Domain.AggregatesModel.CustomerAggregate;
// using CustomerApi.Domain.AggregatesModel.CustomerAggregate.Rules;
// using CustomerApi.Data.v1.Rules;
// using CustomerApi.Data.v1.Repository;
// using CustomerApi.Service.v1.Command.CreateCustomer;
// using CustomerApi.Domain.SeekWork;

// namespace CustomerApi.Services.Tests.v1
// {
//     public class CreateCustomerCommandTest
//     {
//         [Fact]
//         public void CreateCustomerCommand_CheckExistCustomer_Test()
//         {
//             //Arange
//             var existCustomer = Customer.CreateCustomer(
//                 Guid.Parse("9f35b48d-cb87-4783-bfdb-21e36012930a"),
//                 "Amin",
//                 "Ziagham",
//                 "amin.ziagham@gmail.com",
//                 new DateTime(1985, 04, 06)
//             );

//             var _customerRepository = new Mock<ICustomerRepository>();
//             _customerRepository.Setup(p => p.GetCustomerByIdAsync(Guid.Parse("9f35b48d-cb87-4783-bfdb-21e36012930a"), new System.Threading.CancellationToken())).Returns(Task.FromResult(existCustomer)); 
//             _customerRepository.Setup(p => p.IsEmailUniqueAsync("amin.ziagham@gmail.com")).Returns(Task.FromResult(true)); 

//             var _customerUniquenessChecker = new Mock<ICustomerUniquenessChecker>();

//             _customerUniquenessChecker.Setup(p => p.IsUnique("amin.ziagham@gmail.com")).Returns(Task.FromResult(true)); 

//             CreateCustomerCommand command = new CreateCustomerCommand("Amin", "Ziagham", "amin.ziagham@gmail.com", new DateTime(1985, 04, 06));
//             var handler = new CreateCustomerCommandHandler(_customerRepository.Object, _customerUniquenessChecker.Object);

//             //Act
//             //var result = await handler.Handle(command, new System.Threading.CancellationToken());
//             Assert.ThrowsAsync<BusinessRuleValidationException>(() => handler.Handle(command, new System.Threading.CancellationToken()));
 
//             //Asert

//             //Assert.Equal("Customer with this email already exists.", ex.Result.Message);


//             //Assert.IsType<BusinessRuleValidationException>(result);
//             // Assert.IsType(typeof(BusinessRuleValidationException), result.GetType());
//         }

//         [Fact]
//         public async void CreateCustomerCommand_NewCustomer_Test()
//         {
//             //Arange
//             var testCustomer = Customer.CreateCustomer(
//                 Guid.Parse("9f35b48d-cb87-4783-bfdb-21e36012930a"),
//                 "Amin",
//                 "Ziagham",
//                 "amin.ziagham@gmail.com",
//                 new DateTime(1985, 04, 06)
//             );

//             var newCustomer = Customer.CreateCustomer(
//                 Guid.Parse("e3d6ee6d-856b-443c-a1cb-72eaa1b56cc6"),
//                 "Amin",
//                 "Ziagham",
//                 "amin.ziagham2@gmail.com",
//                 new DateTime(1985, 04, 06)
//             );

//             var _customerRepository = new Mock<ICustomerRepository>();
//             _customerRepository.Setup(p => p.GetCustomerByIdAsync(Guid.Parse("9f35b48d-cb87-4783-bfdb-21e36012930a"), new System.Threading.CancellationToken())).Returns(Task.FromResult(existCustomer)); 
//             _customerRepository.Setup(p => p.IsEmailUniqueAsync("amin.ziagham@gmail.com")).Returns(Task.FromResult(true)); 
//             //_customerRepository.Setup(p => p.AddAsync(newCustomer)).Returns(Task.FromResult(newCustomer)); 

//             var _customerUniquenessChecker = new Mock<ICustomerUniquenessChecker>();

//             _customerUniquenessChecker.Setup(p => p.IsUnique("amin.ziagham@gmail.com")).Returns(Task.FromResult(true)); 

//             CreateCustomerCommand command = new CreateCustomerCommand("Amin2", "Ziagham2", "amin2.ziagham2@gmail.com", new DateTime(1985, 04, 06));
//             var handler = new CreateCustomerCommandHandler(_customerRepository.Object, _customerUniquenessChecker.Object);

//             Customer c = await handler.Handle(command, new System.Threading.CancellationToken());

//             //Asert
//             Assert.IsType<Customer>(c);
 
//         }
//     }
// }
