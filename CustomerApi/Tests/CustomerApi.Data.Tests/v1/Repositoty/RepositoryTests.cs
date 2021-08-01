using System;
using System.Linq;
using CustomerApi.Data.Database.v1;
using CustomerApi.Data.Repository.v1;
using CustomerApi.Domain.AggregatesModel.CustomerAggregate;
using CustomerApi.Data.Tests.Infrastructure;
using FakeItEasy;
using FluentAssertions;
using Xunit;

namespace CustomerApi.Data.Tests.v1.Repository
{
    public class RepositoryTests : DatabaseTestBase
    {
        private readonly CustomerContext _CustomerContext;
        private readonly Repository<Customer> _testee;
        private readonly Repository<Customer> _testeeFake;
        private readonly Customer _newCustomer;

        public RepositoryTests()
        {
            _CustomerContext = A.Fake<CustomerContext>();
            _testeeFake = new Repository<Customer>(_CustomerContext);
            _testee = new Repository<Customer>(Context);
            _newCustomer = new Customer
            {
                Id = Guid.Parse("9f35b48d-cb87-4783-bfdb-21e36012930b"),
                FirstName = "Amin",
                LastName = "Ziagham",
                Email = "amin.ziagham@gmail.com",
                BirthDate = new DateTime(1985, 04, 06),
                Active = true
            };
        }

        [Theory]
        [InlineData("Changed")]
        public async void UpdateCustomerAsync_WhenCustomerIsNotNull_ShouldReturnCustomer(string firstName)
        {
            var customer = Context.Customers.First();
            customer.FirstName = firstName;

            var result = await _testee.UpdateAsync(customer);

            result.Should().BeOfType<Customer>();
            result.FirstName.Should().Be(firstName);
        }

        [Fact]
        public void AddAsync_WhenEntityIsNull_ThrowsException()
        {
            _testee.Invoking(x => x.AddAsync(null)).Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void AddAsync_WhenExceptionOccurs_ThrowsException()
        {
            A.CallTo(() => _CustomerContext.SaveChangesAsync(default)).Throws<Exception>();

            _testeeFake.Invoking(x => x.AddAsync(new Customer())).Should().Throw<Exception>().WithMessage("entity could not be saved: Exception of type 'System.Exception' was thrown.");
        }

        [Fact]
        public async void CreateCustomerAsync_WhenCustomerIsNotNull_ShouldReturnCustomer()
        {
            var result = await _testee.AddAsync(_newCustomer);

            result.Should().BeOfType<Customer>();
        }

        [Fact]
        public async void CreateCustomerAsync_WhenCustomerIsNotNull_ShouldShouldAddCustomer()
        {
            var CustomerCount = Context.Customers.Count();

            await _testee.AddAsync(_newCustomer);

            Context.Customers.Count().Should().Be(CustomerCount + 1);
        }

        // [Fact]
        // public void GetAll_WhenExceptionOccurs_ThrowsException()
        // {
        //     A.CallTo(() => _CustomerContext.Set<Customer>()).Throws<Exception>();

        //     _testeeFake.Invoking(x => x.GetAll()).Should().Throw<Exception>().WithMessage("Couldn't retrieve entities: Exception of type 'System.Exception' was thrown.");
        // }

        [Fact]
        public void UpdateAsync_WhenEntityIsNull_ThrowsException()
        {
            _testee.Invoking(x => x.UpdateAsync(null)).Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void UpdateAsync_WhenExceptionOccurs_ThrowsException()
        {
            A.CallTo(() => _CustomerContext.SaveChangesAsync(default)).Throws<Exception>();

            _testeeFake.Invoking(x => x.UpdateAsync(new Customer())).Should().Throw<Exception>().WithMessage("entity could not be updated Exception of type 'System.Exception' was thrown.");
        }
    }
}