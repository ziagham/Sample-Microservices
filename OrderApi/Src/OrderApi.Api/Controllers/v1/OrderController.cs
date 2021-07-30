using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderApi.Service.v1.Command.CreateOrder;
using OrderApi.Service.v1.Command.PayOrder;
using OrderApi.Service.v1.Query.GetOrderById;
using OrderApi.Service.v1.Query.GetPaidOrder;
using OrderApi.Domain.AggregatesModel.OrderAggregate;

namespace OrderApi.Api.Controllers
{
    [Produces("application/json")]
    [ApiVersion("1.0")]
    public class OrderController : BaseApiController
    {
        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
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

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
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