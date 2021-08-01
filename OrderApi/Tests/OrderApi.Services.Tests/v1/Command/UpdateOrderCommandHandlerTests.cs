using System.Collections.Generic;
using FakeItEasy;
using OrderApi.Data.v1.Repository;
using OrderApi.Domain.AggregatesModel.OrderAggregate;
using OrderApi.Services.v1.Features.Command.UpdateOrder;
using Xunit;

namespace OrderApi.Service.Test.v1.Command
{
    public class UpdateOrderCommandHandlerTests
    {
        private readonly UpdateOrderCommandHandler _testee;
        private readonly IOrderRepository _orderRepository;

        public UpdateOrderCommandHandlerTests()
        {
            _orderRepository = A.Fake<IOrderRepository>();
            _testee = new UpdateOrderCommandHandler(_orderRepository);
        }

        [Fact]
        public async void Handle_ShouldCallRepositoryAddAsync()
        {
            await _testee.Handle(new UpdateOrderCommand(), default);

            A.CallTo(() => _orderRepository.UpdateRangeAsync(A<List<Order>>._)).MustHaveHappenedOnceExactly();
        }
    }
}