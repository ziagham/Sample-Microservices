using System.Threading;
using System.Threading.Tasks;
using OrderApi.Data.v1.Repository;
using MediatR;

namespace OrderApi.Services.v1.Features.Command.UpdateOrder
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