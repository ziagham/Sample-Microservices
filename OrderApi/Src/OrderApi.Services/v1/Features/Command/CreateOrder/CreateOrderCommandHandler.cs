using System.Threading;
using System.Threading.Tasks;
using OrderApi.Data.Repository.v1;
using MediatR;
using OrderApi.Domain.AggregatesModel.OrderAggregate;

namespace OrderApi.Service.v1.Command.CreateOrder
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Order>
    {
        private readonly IOrderRepository _orderRepository;

        public CreateOrderCommandHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<Order> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var createOrder = Order.CreateOrder(1, request.CustomerGuid, request.CustomerFullName);
            var customer = await _orderRepository.AddAsync(createOrder);

            return customer;
        }
    }
}