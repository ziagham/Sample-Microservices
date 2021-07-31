using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using OrderApi.Domain.AggregatesModel.OrderAggregate;

namespace OrderApi.Data.v1.Repository
{
    public interface IOrderRepository: IRepository<Order>
    {
        Task<List<Order>> GetPaidOrdersAsync(CancellationToken cancellationToken);
        Task<Order> GetOrderByIdAsync(Guid orderId, CancellationToken cancellationToken);
        Task<List<Order>> GetOrderByCustomerGuidAsync(Guid customerId, CancellationToken cancellationToken);
    }
}