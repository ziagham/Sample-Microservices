using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Moq;
using Microsoft.EntityFrameworkCore;
using CustomerApi.Domain.AggregatesModel.CustomerAggregate;
using CustomerApi.Data.v1.Repository;
using CustomerApi.Data.v1.Database;

namespace CustomerApi.Data.Tests.v1
{
    public class CustomerRepositoryTest
    {
        private readonly Mock<DbSet<Customer>> _dbSetMock;
        private readonly Mock<CustomerContext> _contextMock;
        private readonly List<Customer> _testCustomers;

        public CustomerRepositoryTest()
        {
            _contextMock = new Mock<CustomerContext>();
            _dbSetMock = new Mock<DbSet<Customer>>();

            _testCustomers = new List<Customer>()
            {
                Customer.CreateCustomer(
                    Guid.Parse("9f35b48d-cb87-4783-bfdb-21e36012930a"),
                    "Amin",
                    "Ziagham",
                    "amin.ziagham@gmail.com",
                    new DateTime(1985, 04, 06)
                ),
                Customer.CreateCustomer(
                    Guid.Parse("654b7573-9501-436a-ad36-94c5696ac28f"),
                    "Darth",
                    "Vader",
                    "darth.vader@domain.com",
                     new DateTime(1977, 05, 25)
                ),
                Customer.CreateCustomer(
                    Guid.Parse("971316e1-4966-4426-b1ea-a36c9dde1066"),
                    "Keanu",
                    "Reeves",
                    "keanu.reeves@domain.com",
                    new DateTime(1964, 09, 02)
                )
            };
        }

        // [Fact]
        // public async void RepositoryTest_GetCustomerByIdAsync_Test()
        // {
        //     // Arrange
        //     _dbSetMock.As<IQueryable<Customer>>().Setup(x => x.Provider).Returns(_testCustomers.AsQueryable().Provider);
        //     _dbSetMock.As<IQueryable<Customer>>().Setup(x => x.Expression).Returns(_testCustomers.AsQueryable().Expression);
        //     _dbSetMock.As<IQueryable<Customer>>().Setup(x => x.ElementType).Returns(_testCustomers.AsQueryable().ElementType);
        //     _dbSetMock.As<IQueryable<Customer>>().Setup(x => x.GetEnumerator()).Returns(_testCustomers.AsQueryable().GetEnumerator());

        //     _dbSetMock.Setup(w => w.FirstOrDefaultAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())
        //         .Callback<Guid, CancellationToken>((id, cancellationToken) => _testCustomers.FirstOrDefault(x=>x.Id == id)));
        //         //.Returns(Task.FromResult(0));

        //     //_dbSetMock.Setup(m => m.FirstOrDefaultAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).Callback((Guid id) => _testCustomers.FirstOrDefault(x=>x.Id == id));

        //     // _dbSetMock.Setup(m => m.Add(It.IsAny<Customer>())).Callback((Customer person) => _testCustomers.Add(person));
        //     // _dbSetMock.Setup(m => m.Remove(It.IsAny<Customer>())).Callback((Customer person) => _testCustomers.Remove(person));
            
        //     _contextMock.Setup(x => x.Set<Customer>()).Returns(_dbSetMock.Object);

        //     // Act
        //     var repository = new CustomerRepository(_contextMock.Object);
        //     var id = Guid.Parse("9f35b48d-cb87-4783-bfdb-21e36012930a");
        //     var result = await repository.GetCustomerByIdAsync(id, new System.Threading.CancellationToken());

        //     // Assert
        //     Assert.Equal(_testCustomers[0], result);




            // var testObject = new Customer();
            // _contextMock.Setup(x => x.Set<Customer>()).Returns(_dbSetMock.Object);
            // _dbSetMock.Setup(x => x.FirstOrDefaultAsync(It.IsAny<Customer>())).Returns(testObject);

            // // Act
            // var repository = new CustomerRepository(_contextMock.Object);
            // var id = Guid.Parse("9f35b48d-cb87-4783-bfdb-21e36012930a");
            // var result = await repository.GetCustomerByIdAsync(id, new System.Threading.CancellationToken());

            // //Assert
            // Assert.Equal(_testCustomer, result);

            // var testObject = new Customer();

            // _contextMock.Setup(x => x.Set<Customer>()).Returns(_testCustomers);

            // //_contextMock.Setup(x => x.Set<Customer>()).Returns(_dbSetMock.Object);
            // //_dbSetMock.Setup(x => x.FindAsync(It.IsAny<Guid>())).Returns(_testCustomers);

            // // Act
            // var repository = new CustomerRepository(_contextMock.Object);
            // var id = Guid.Parse("9f35b48d-cb87-4783-bfdb-21e36012930a");
            // var result = await repository.GetCustomerByIdAsync(id, new System.Threading.CancellationToken());

            // // Assert
            // Assert.Equal(_testCustomers[0], result);

            // _contextMock.Verify(x => x.Set<Customer>());
            // _dbSetMock.Verify(x => x.FindAsync(It.IsAny<Guid>()));

            // _dbSetMock.Verify(r => r.CreateDatabase(It.IsAny<string>()), Times.Once); 
            // _dbSetMock.Verify(r => r.UpdateCompanyTable(It.IsAny<Company>()), Times.Once);

    }

    // public class CustomerRepositoryTest
    // {
    //     // [Fact]
    //     // public void CustomerRepository_GetCustomerByIdAsync_Test()
    //     // {
    //     //     // Arrange
    //     //     var testObject = Customer.CreateCustomer(
    //     //         Guid.Parse("9f35b48d-cb87-4783-bfdb-21e36012930a"),
    //     //         "Amin",
    //     //         "Ziagham",
    //     //         "amin.ziagham@gmail.com",
    //     //         new DateTime(1985, 04, 06)
    //     //     );

    //     //     var context = new Mock<CustomerContext>();
    //     //     var dbSetMock = new Mock<DbSet<Customer>>();

    //     //     context.Setup(x => x.Set<Customer>()).Returns(dbSetMock.Object);
    //     //     dbSetMock.Setup(x => x.FirstOrDefault(It.IsAny<bool>())).Returns(testObject);

    //     //     // Act
    //     //     var repository = new CustomerRepository(context.Object);
    //     //     repository.GetCustomerByIdAsync(Guid.Parse("9f35b48d-cb87-4783-bfdb-21e36012930a"), new System.Threading.CancellationToken());

    //     //     // Assert
    //     //     context.Verify(x => x.Set<Customer>());
    //     //     dbSetMock.Verify(x => x.FirstOrDefault(It.IsAny<bool>()));
    //     // }

    //     // [Fact]
    //     // public async void CustomerRepository_AddAsync_Test()
    //     // {
    //     //     // Arrange
    //     //     var testObject = Customer.CreateCustomer(
    //     //         Guid.Parse("9f35b48d-cb87-4783-bfdb-21e36012930a"),
    //     //         "Amin",
    //     //         "Ziagham",
    //     //         "amin.ziagham@gmail.com",
    //     //         new DateTime(1985, 04, 06)
    //     //     );

    //     //     var context = new Mock<CustomerContext>();
    //     //     var dbSetMock = new Mock<DbSet<Customer>>();

    //     //     context.Setup(x => x.Set<Customer>()).Returns(dbSetMock.Object);
    //     //     // dbSetMock.Setup(x => x.Add(It.IsAny<Customer>())).Returns(testObject);

    //     //     // Act
    //     //     var repository = new CustomerRepository(context.Object);
    //     //     await repository.AddAsync(testObject);

    //     //     //Assert
    //     //     // dbSetMock.Verify(m => m.Add(It.IsAny<Customer>()), Times.Once());
    //     //     // context.Verify(m => m.SaveChanges(), Times.Once());

    //     //     context.Verify(x => x.Set<Customer>());
    //     //     dbSetMock.Verify(x => x.Add(It.Is<Customer>(y => y == testObject)));
    //     // }

    //     // [Fact]
    //     // public void CustomerRepository_GetAll_Test()
    //     // {
    //     //     // Arrange
    //     //     var testObject = Customer.CreateCustomer(
    //     //         Guid.Parse("9f35b48d-cb87-4783-bfdb-21e36012930a"),
    //     //         "Amin",
    //     //         "Ziagham",
    //     //         "amin.ziagham@gmail.com",
    //     //         new DateTime(1985, 04, 06)
    //     //     );
    //     //     var testList = new List<Customer>() { testObject };

    //     //     var dbSetMock = new Mock<DbSet<Customer>>();
    //     //     dbSetMock.As<IQueryable<Customer>>().Setup(x => x.Provider).Returns(testList.AsQueryable().Provider);
    //     //     dbSetMock.As<IQueryable<Customer>>().Setup(x => x.Expression).Returns(testList.AsQueryable().Expression);
    //     //     dbSetMock.As<IQueryable<Customer>>().Setup(x => x.ElementType).Returns(testList.AsQueryable().ElementType);
    //     //     dbSetMock.As<IQueryable<Customer>>().Setup(x => x.GetEnumerator()).Returns(testList.AsQueryable().GetEnumerator());

    //     //     var context = new Mock<CustomerContext>();
    //     //     context.Setup(x => x.Set<Customer>()).Returns(dbSetMock.Object);

    //     //     // Act
    //     //     var repository = new CustomerRepository(context.Object);
    //     //     var result = repository.GetAll();

    //     //     // Assert
    //     //     Assert.Equal(testList, result.ToList());
    //     // }
    // }
}
