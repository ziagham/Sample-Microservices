using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using OrderApi.Data.Repository.v1;
using MediatR;
using OrderApi.Domain.AggregatesModel.OrderAggregate;
using System;

namespace OrderApi.Services.v1.Features.Query.GetOrderByCustomer
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