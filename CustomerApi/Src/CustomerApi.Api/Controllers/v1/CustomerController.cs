using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CustomerApi.Domain.AggregatesModel.CustomerAggregate;
using CustomerApi.Services.v1.Features.Command.CreateCustomer;
using CustomerApi.Services.v1.Features.Command.UpdateCustomer;
using CustomerApi.Services.v1.Features.Query.GetCustomerById;
using CustomerApi.Services.v1.Features.Query.GetCustomers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CustomerApi.Api.Controllers.v1
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