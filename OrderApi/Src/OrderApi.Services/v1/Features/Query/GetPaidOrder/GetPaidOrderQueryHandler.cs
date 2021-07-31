using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OrderApi.Data.v1.Repository;
using OrderApi.Domain.AggregatesModel.OrderAggregate;

namespace OrderApi.Service.v1.Query.GetPaidOrder
{
    public class GetPaidOrderQueryHandler : IRequestHandler<GetPaidOrderQuery, List<Order>>
    {
        private readonly IOrderRepository _orderRepository;

        public GetPaidOrderQueryHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<List<Order>> Handle(GetPaidOrderQuery request, CancellationToken cancellationToken)
        {
            return await _orderRepository.GetPaidOrdersAsync(cancellationToken);
        }
    }
}