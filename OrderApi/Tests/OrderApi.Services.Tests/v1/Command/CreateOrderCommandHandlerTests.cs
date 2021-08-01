using FakeItEasy;
using FluentAssertions;
using OrderApi.Data.v1.Repository;
using OrderApi.Domain.AggregatesModel.OrderAggregate;
using OrderApi.Services.v1.Features.Command.CreateOrder;
using Xunit;

namespace OrderApi.Services.Tests.v1.Command
{
    public class CreateOrderCommandHandlerTests
    {
        private readonly IOrderRepository _orderRepository;
        private readonly CreateOrderCommandHandler _testee;

        public CreateOrderCommandHandlerTests()
        {
            _orderRepository = A.Fake<IOrderRepository>();
            _testee = new CreateOrderCommandHandler(_orderRepository);
        }

        [Fact]
        public async void Handle_ShouldReturnCreatedOrder()
        {
            A.CallTo(() => _orderRepository.AddAsync(A<Order>._)).Returns(new Order { CustomerFullName = "Reza Ziagham" });

            var result = await _testee.Handle(new CreateOrderCommand(), default);

            result.Should().BeOfType<Order>();
            result.CustomerFullName.Should().Be("Reza Ziagham");
        }

        [Fact]
        public async void Handle_ShouldCallRepositoryAddAsync()
        {
            await _testee.Handle(new CreateOrderCommand(), default);

            A.CallTo(() => _orderRepository.AddAsync(A<Order>._)).MustHaveHappenedOnceExactly();
        }
    }
}