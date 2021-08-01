using System.Collections.Generic;
using MediatR;
using OrderApi.Domain;
using OrderApi.Domain.AggregatesModel.OrderAggregate;

namespace OrderApi.Services.v1.Features.Query.GetPaidOrder
{
    public class GetPaidOrderQuery : IRequest<List<Order>>
    {
    }
}