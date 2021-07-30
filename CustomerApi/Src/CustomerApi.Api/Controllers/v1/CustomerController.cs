using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CustomerApi.Domain.AggregatesModel.CustomerAggregate;
using CustomerApi.Service.v1.Command.CreateCustomer;
using CustomerApi.Service.v1.Command.UpdateCustomer;
using CustomerApi.Service.v1.Query.GetCustomerById;
using CustomerApi.Service.v1.Query.GetCustomers;
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