using OrderApi.Domain.SeekWork;
using System;

namespace OrderApi.Domain.AggregatesModel.OrderAggregate
{
    public class Order : Entity
    {
        public int OrderState { get; set; }
        public Guid CustomerGuid { get; set; }
        public string CustomerFullName { get; set; }

        public Order()
        {
            this.CreatedUtc = DateTime.Now;
        }
    }
}
