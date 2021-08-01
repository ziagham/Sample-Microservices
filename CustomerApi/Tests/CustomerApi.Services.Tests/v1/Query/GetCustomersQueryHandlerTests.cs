using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CustomerApi.Domain.AggregatesModel.CustomerAggregate;
using CustomerApi.Data.v1.Repository;
using CustomerApi.Services.v1.Features.Query.GetCustomers;
using FakeItEasy;
using FluentAssertions;
using Xunit;

namespace CustomerApi.Services.Tests.v1.Query
{
    public class GetCustomersQueryHandlerTests
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly GetCustomersQueryHandler _testee;
        private readonly List<Customer> _customers;

        public GetCustomersQueryHandlerTests()
        {
            _customerRepository = A.Fake<ICustomerRepository>();
            _testee = new GetCustomersQueryHandler(_customerRepository);

            _customers = new List<Customer>
            {
                new Customer
                {
                    Id = new Guid(),
                    Email = "test.customer@domain.com"
                },
                new Customer
                {
                    Id = new Guid(),
                    Email = "test2.customer@domain.com"
                }
            };
        }

        [Fact]
        public async Task Handle_ShouldReturnCustomers()
        {
            A.CallTo(() => _customerRepository.GetAll()).Returns(_customers);

            var result = await _testee.Handle(new GetCustomersQuery(), default);

            A.CallTo(() => _customerRepository.GetAll()).MustHaveHappenedOnceExactly();
            result.Should().BeOfType<List<Customer>>();
            result.Count.Should().Be(2);
        }
    }
}