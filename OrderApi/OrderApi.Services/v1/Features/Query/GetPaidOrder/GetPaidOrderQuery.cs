using System.Collections.Generic;
using MediatR;
using OrderApi.Domain;
using OrderApi.Domain.AggregatesModel.OrderAggregate;

namespace OrderApi.Service.v1.Query.GetPaidOrder
{
    public class GetPaidOrderQuery : IRequest<List<Order>>
    {
    }
}