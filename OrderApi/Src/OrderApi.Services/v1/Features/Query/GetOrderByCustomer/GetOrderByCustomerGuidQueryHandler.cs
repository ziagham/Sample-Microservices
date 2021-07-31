using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using OrderApi.Data.v1.Repository;
using MediatR;
using OrderApi.Domain.AggregatesModel.OrderAggregate;
using System;

namespace OrderApi.Service.v1.Query.GetOrderByCustomer
{
    public class GetOrderByCustomerGuidQueryHandler : IRequestHandler<GetOrderByCustomerGuidQuery, List<Order>>
    {
        private readonly IOrderRepository _orderRepository;

        public GetOrderByCustomerGuidQueryHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<List<Order>> Handle(GetOrderByCustomerGuidQuery request, CancellationToken cancellationToken)
        {
            return await _orderRepository.GetOrderByCustomerGuidAsync(request.CustomerId, cancellationToken);
        }
    }
}