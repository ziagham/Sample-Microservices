using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OrderApi.Data.v1.Repository;
using OrderApi.Domain.AggregatesModel.OrderAggregate;

namespace OrderApi.Service.v1.Command.PayOrder
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
            var updatedOrder = Order.UpdateOrder(request.Id, request.OrderState, request.CustomerGuid, request.CustomerFullName);
            var order = await _orderRepository.UpdateAsync(updatedOrder);

            return order;
        }
    }
}