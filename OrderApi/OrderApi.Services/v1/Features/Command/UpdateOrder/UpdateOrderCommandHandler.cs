using System.Threading;
using System.Threading.Tasks;
using OrderApi.Data.Repository.v1;
using MediatR;
using OrderApi.Service.v1.Command.UpdateOrder;

namespace OrderApi.Service.v1.Command.UpdateCustomer
{
    public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand>
    {
        private readonly IOrderRepository _orderRepository;

        public UpdateOrderCommandHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<Unit> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            await _orderRepository.UpdateRangeAsync(request.Orders);

            return Unit.Value;
        }
    }
}