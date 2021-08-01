using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OrderApi.Data.Repository.v1;
using OrderApi.Domain.AggregatesModel.OrderAggregate;

namespace OrderApi.Services.v1.Features.Command.PayOrder
{
    public class PayOrderCommandHandler : IRequestHandler<PayOrderCommand, Order>
    {
        private readonly IOrderRepository _orderRepository;

        public PayOrderCommandHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<Order> Handle(PayOrderCommand request, CancellationToken cancellationToken)
        {
            var updatedOrder = new Order {
                Id = request.Id,
                OrderState = request.OrderState,
                CustomerGuid = request.CustomerGuid,
                CustomerFullName = request.CustomerFullName
            };

            var order = await _orderRepository.UpdateAsync(updatedOrder);

            return order;
        }
    }
}