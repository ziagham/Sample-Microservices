using OrderApi.Domain.SeekWork;
using System;

namespace OrderApi.Domain.AggregatesModel.OrderAggregate
{
    public class Order : Entity
    {
        #region properties
        public int OrderState { get; set; }
        public Guid CustomerGuid { get; set; }
        public string CustomerFullName { get; set; }
        #endregion

        #region constructors
        public Order()
        {
            this.CreatedUtc = DateTime.Now;
        }
        #endregion
    }
}
