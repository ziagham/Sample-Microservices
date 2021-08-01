using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderApi.Services.v1.Features.Command.CreateOrder;
using OrderApi.Services.v1.Features.Command.PayOrder;
using OrderApi.Services.v1.Features.Query.GetOrderById;
using OrderApi.Services.v1.Features.Query.GetPaidOrder;
using OrderApi.Domain.AggregatesModel.OrderAggregate;

namespace OrderApi.Api.Controllers.v1
{
    [Produces("application/json")]
    [ApiVersion("1.0")]
    public class OrderController : BaseApiController
    {
        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<Order>>> Get()
        {
            try
            {
                return await _mediator.Send(new GetPaidOrderQuery());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Order>> Post(CreateOrderCommand createOrderCommand)
        {
            try
            {
                return await _mediator.Send(createOrderCommand);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("Pay/{id}")]
        public async Task<ActionResult<Order>> Pay(Guid id)
        {
            try
            {
                var order = await _mediator.Send(new GetOrderByIdQuery
                {
                    Id = id
                });

                if (order == null)
                {
                    return BadRequest($"No order found with the id {id}");
                }

                order.OrderState = 2;

                return await _mediator.Send(new PayOrderCommand
                {
                    Id = order.Id,
                    OrderState = order.OrderState,
                    CustomerGuid = order.CustomerGuid,
                    CustomerFullName = order.CustomerFullName
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}