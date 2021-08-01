using System.Threading;
using System.Threading.Tasks;
using OrderApi.Data.v1.Repository;
using MediatR;
using OrderApi.Domain.AggregatesModel.OrderAggregate;

namespace OrderApi.Services.v1.Features.Command.CreateOrder
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
            var createOrder = new Order {
                OrderState = 1,
                CustomerGuid = request.CustomerGuid,
                CustomerFullName = request.CustomerFullName
            };
            
            var customer = await _orderRepository.AddAsync(createOrder);

            return customer;
        }
    }
}