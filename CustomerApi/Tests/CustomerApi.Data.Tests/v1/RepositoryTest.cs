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
    public class RepositoryTest
    {
        
        private readonly Mock<DbSet<Customer>> _dbSetMock;
        private readonly Mock<CustomerContext> _contextMock;
        private readonly List<Customer> _testCustomers;

        public RepositoryTest()
        {
            _contextMock = new Mock<CustomerContext>();
            _dbSetMock = new Mock<DbSet<Customer>>();

            _testCustomers = InitialTestData();
        }

        private List<Customer> InitialTestData()
        {
            var result = new List<Customer>()
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
            return result;
        }
        
        [Fact]
        public void RepositoryTest_AddAsync_NullException_Test()
        {
            // Arrange
            _contextMock.Setup(x => x.Set<Customer>()).Returns(_dbSetMock.Object);

            // Act
            var repository = new Repository<Customer>(_contextMock.Object);

            //Assert
            Assert.ThrowsAsync<ArgumentNullException>(() => repository.AddAsync(null));

        }

        [Fact]
        public async void RepositoryTest_AddAsync_Test()
        {
            // Arrange
            _dbSetMock.Setup(m => m.AddAsync(It.IsAny<Customer>(), It.IsAny<CancellationToken>()))
            .Callback<Customer, CancellationToken>((customer, cancellationToken) => 
            {
                _testCustomers.Add(customer);
            });

            var testObject = Customer.CreateCustomer(
                Guid.Parse("9f35b48d-cb87-4783-bfdb-21e36012930b"),
                "Amin",
                "Ziagham",
                "amin.ziagham2@gmail.com",
                new DateTime(1985, 04, 06)
            );

            _contextMock.Setup(x => x.Set<Customer>()).Returns(_dbSetMock.Object);

            // Act
            var repository = new Repository<Customer>(_contextMock.Object);
            var result = await repository.AddAsync(testObject);

            //Assert
            Assert.Equal(4, _testCustomers.Count());
        }

        [Fact]
        public void RepositoryTest_GetAll_Test()
        {
            // Arrange
            _dbSetMock.As<IQueryable<Customer>>().Setup(x => x.Provider).Returns(_testCustomers.AsQueryable().Provider);
            _dbSetMock.As<IQueryable<Customer>>().Setup(x => x.Expression).Returns(_testCustomers.AsQueryable().Expression);
            _dbSetMock.As<IQueryable<Customer>>().Setup(x => x.ElementType).Returns(_testCustomers.AsQueryable().ElementType);
            _dbSetMock.As<IQueryable<Customer>>().Setup(x => x.GetEnumerator()).Returns(_testCustomers.AsQueryable().GetEnumerator());

            _contextMock.Setup(x => x.Set<Customer>()).Returns(_dbSetMock.Object);

            // Act
            var repository = new CustomerRepository(_contextMock.Object);
            var result = repository.GetAll();

            // Assert
            Assert.Equal(_testCustomers, result.ToList());
        }
    }
}
