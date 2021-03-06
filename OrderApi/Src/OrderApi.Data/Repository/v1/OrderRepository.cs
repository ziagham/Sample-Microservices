using System;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using OrderApi.Data.Database.v1;
using System.Collections.Generic;
using OrderApi.Domain.AggregatesModel.OrderAggregate;
using Microsoft.EntityFrameworkCore;

namespace OrderApi.Data.Repository.v1
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(OrderContext orderContext) : base(orderContext)
        {}

        public async Task<List<Order>> GetPaidOrdersAsync(CancellationToken cancellationToken)
        {
            return await OrderContext.Orders.Where(x => x.OrderState == 2).ToListAsync( cancellationToken);
        }

        public async Task<Order> GetOrderByIdAsync(Guid orderId, CancellationToken cancellationToken)
        {
            return await OrderContext.Orders.FirstOrDefaultAsync(x => x.Id == orderId, cancellationToken);
        }

        public async Task<List<Order>> GetOrderByCustomerGuidAsync(Guid customerId, CancellationToken cancellationToken)
        {
            return await OrderContext.Orders.Where(x => x.CustomerGuid == customerId).ToListAsync(cancellationToken);
        }
    }
}