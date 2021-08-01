using System;
using MediatR;
using OrderApi.Domain.AggregatesModel.OrderAggregate;

namespace OrderApi.Services.v1.Features.Command.PayOrder
{
   public class PayOrderCommand : IRequest<Order>
    {
        public Guid Id { get; set; }
        public int OrderState { get; set; }
        public Guid CustomerGuid { get; set; }
        public string CustomerFullName { get; set; }
    }
}