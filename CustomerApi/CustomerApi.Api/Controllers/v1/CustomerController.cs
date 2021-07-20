using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CustomerApi.Domain.AggregatesModel.CustomerAggregate;
using CustomerApi.Service.v1.Command;
using CustomerApi.Service.v1.Query;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CustomerApi.Api.Controllers
{
    [Produces("application/json")]
    [ApiVersion("1.0")]
    public class CustomerController : BaseApiController
    {
        public CustomerController(IMediator mediator)
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
        public async Task<ActionResult<List<Customer>>> Get()
        {
            try
            {
                return await _mediator.Send(new GetCustomersQuery());
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
        public async Task<ActionResult<Customer>> Post(CreateCustomerCommand createCustomerCommand)
        {
            try
            {
                return await _mediator.Send(createCustomerCommand);
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
        [HttpPut]
        public async Task<ActionResult<Customer>> Put(UpdateCustomerCommand updateCustomerCommand)
        {
            try
            {
                var customer = await _mediator.Send(new GetCustomerByIdQuery
                {
                    Id = updateCustomerCommand.Id
                });

                if (customer == null)
                {
                    return BadRequest($"No customer found with the id {updateCustomerCommand.Id}");
                }

                return await _mediator.Send(updateCustomerCommand);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}