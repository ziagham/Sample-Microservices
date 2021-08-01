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
        // Empty constructor for EF
        public Order() { }

        // public Order(Guid id, int orderState, Guid customerGuid, string customerFullName)
        // {
        //     Id = id;
        //     OrderState = orderState;
        //     CustomerGuid = customerGuid;
        //     CustomerFullName = customerFullName;
        // }
        #endregion

        // #region methods

        // public static Order CreateOrder(int orderState, Guid customerGuid, string customerFullName)
        // {
        //     return new Order(Guid.NewGuid(), orderState, customerGuid, customerFullName);
        // }

        // public static Order UpdateOrder(Guid id, int orderState, Guid customerGuid, string customerFullName)
        // {
        //     return new Order(id, orderState, customerGuid, customerFullName);
        // }
        // #endregion
    }
}
