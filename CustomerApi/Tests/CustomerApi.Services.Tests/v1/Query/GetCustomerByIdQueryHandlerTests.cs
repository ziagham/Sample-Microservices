using System;
using System.Threading.Tasks;
using CustomerApi.Domain.AggregatesModel.CustomerAggregate;
using CustomerApi.Data.Repository.v1;
using CustomerApi.Services.v1.Features.Query.GetCustomerById;
using FakeItEasy;
using FluentAssertions;
using Xunit;

namespace CustomerApi.Services.Tests.v1.Query
{
    public class GetCustomerByIdQueryHandlerTests
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly GetCustomerByIdQueryHandler _testee;
        private readonly Customer _customer;
        private readonly Guid _id = Guid.Parse("803a95ef-89c5-43d5-aa2c-82a3695d9894");

        public GetCustomerByIdQueryHandlerTests()
        {
            _customerRepository = A.Fake<ICustomerRepository>();
            _testee = new GetCustomerByIdQueryHandler(_customerRepository);

            _customer = new Customer { Id = _id, Email = "test.customer@domain.com" };
        }

        [Fact]
        public async Task Handle_WithValidId_ShouldReturnCustomer()
        {
            A.CallTo(() => _customerRepository.GetCustomerByIdAsync(_id, default)).Returns(_customer);

            var result = await _testee.Handle(new GetCustomerByIdQuery { Id = _id }, default);

            A.CallTo(() => _customerRepository.GetCustomerByIdAsync(_id, default)).MustHaveHappenedOnceExactly();
            result.Email.Should().Be("test.customer@domain.com");
        }
    }
}