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

        /// <summary>
        /// Action to see all existing customers.
        /// </summary>
        /// <returns>Returns a list of all customers</returns>
        /// <response code="200">Returned if the customers were loaded</response>
        /// <response code="400">Returned if the customers couldn't be loaded</response>
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

        /// <summary>
        /// Action to create a new customer in the database.
        /// </summary>
        /// <param name="createCustomerModel">Model to create a new customer</param>
        /// <returns>Returns the created customer</returns>
        /// <response code="200">Returned if the customer was created</response>
        /// <response code="400">Returned if the model couldn't be parsed or the customer couldn't be saved</response>
        /// <response code="422">Returned when the validation failed</response>
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

        /// <summary>
        /// Action to update an existing customer
        /// </summary>
        /// <param name="updateCustomerModel">Model to update an existing customer.</param>
        /// <returns>Returns the updated customer</returns>
        /// <response code="200">Returned if the customer was updated</response>
        /// <response code="400">Returned if the model couldn't be parsed or the customer couldn't be found</response>
        /// <response code="422">Returned when the validation failed</response>
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



        // public async Task<ActionResult<Customer>> Put(UpdateCustomerCommand updateCustomerCommand)
        // {
        //     try
        //     {
        //         var customer = await _mediator.Send(new GetCustomerByIdQuery
        //         {
        //             Id = updateCustomerCommand.Id
        //         });

        //         if (customer == null)
        //         {
        //             return BadRequest($"No customer found with the id {updateCustomerCommand.Id}");
        //         }

        //         return await _mediator.Send(updateCustomerCommand);
        //     }
        //     catch (Exception ex)
        //     {
        //         return BadRequest(ex.Message);
        //     }
        // }
    }
}