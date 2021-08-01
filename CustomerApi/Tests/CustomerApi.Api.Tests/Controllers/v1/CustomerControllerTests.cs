using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using CustomerApi.Api.Controllers.v1;
using CustomerApi.Domain.AggregatesModel.CustomerAggregate;
using CustomerApi.Services.v1.Features.Command.CreateCustomer;
using CustomerApi.Services.v1.Features.Command.UpdateCustomer;
using CustomerApi.Services.v1.Features.Query.GetCustomerById;
using CustomerApi.Services.v1.Features.Query.GetCustomers;
using FakeItEasy;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace CustomerApi.Test.Controllers.v1
{
    public class CustomerControllerTests
    {
        private readonly IMediator _mediator;
        private readonly CustomerController _testee;
        private readonly CreateCustomerCommand _createCustomerModel;
        private readonly UpdateCustomerCommand _updateCustomerModel;
        private readonly Guid _id = Guid.Parse("5224ed94-6d9c-42ec-ba93-dfb11fe68931");

        public CustomerControllerTests()
        {
            _mediator = A.Fake<IMediator>();
            _testee = new CustomerController(_mediator);

            _createCustomerModel = new CreateCustomerCommand
            {
                FirstName = "Amin",
                LastName = "Ziagham",
                Email = "amin.ziagham@gmail.com",
                BirthDate = new DateTime(1985, 04, 06)
            };
            _updateCustomerModel = new UpdateCustomerCommand
            {
                Id = _id,
                FirstName = "Amin",
                LastName = "Ziagham",
                Email = "amin.ziagham@gmail.com",
                BirthDate = new DateTime(1985, 04, 06)
            };
            var customer = new List<Customer>
            {
                new Customer
                {
                    Id = _id,
                    FirstName = "Amin",
                    LastName = "Ziagham",
                    Email = "amin.ziagham@gmail.com",
                    BirthDate = new DateTime(1985, 04, 06)
                },
                new Customer
                {
                    Id = Guid.Parse("654b7573-9501-436a-ad36-94c5696ac28f"),
                    FirstName = "Darth",
                    LastName = "Vader",
                    Email = "darth.vader@domain.com",
                    BirthDate = new DateTime(1977, 05, 25),
                }
            };

            A.CallTo(() => _mediator.Send(A<CreateCustomerCommand>._, default)).Returns(customer.First());
            A.CallTo(() => _mediator.Send(A<UpdateCustomerCommand>._, default)).Returns(customer.First());
            A.CallTo(() => _mediator.Send(A<GetCustomersQuery>._, default)).Returns(customer);
        }

        [Theory]
        [InlineData("CreateCustomerAsync: customer is null")]
        public async void Post_WhenAnExceptionOccurs_ShouldReturnBadRequest(string exceptionMessage)
        {
            A.CallTo(() => _mediator.Send(A<CreateCustomerCommand>._, default)).Throws(new ArgumentException(exceptionMessage));

            var result = await _testee.Post(_createCustomerModel);

            (result.Result as StatusCodeResult)?.StatusCode.Should().Be((int) HttpStatusCode.BadRequest);
            (result.Result as BadRequestObjectResult)?.Value.Should().Be(exceptionMessage);
        }

        [Theory]
        [InlineData("UpdateCustomerAsync: customer is null")]
        [InlineData("No user with this id found")]
        public async void Put_WhenAnExceptionOccurs_ShouldReturnBadRequest(string exceptionMessage)
        {
            A.CallTo(() => _mediator.Send(A<UpdateCustomerCommand>._, default)).Throws(new Exception(exceptionMessage));

            var result = await _testee.Put(_updateCustomerModel);

            (result.Result as StatusCodeResult)?.StatusCode.Should().Be((int) HttpStatusCode.BadRequest);
            (result.Result as BadRequestObjectResult)?.Value.Should().Be(exceptionMessage);
        }

        [Fact]
        public async void Get_ShouldReturnCustomers()
        {
            var result = await _testee.Get();

            (result.Result as StatusCodeResult)?.StatusCode.Should().Be((int) HttpStatusCode.OK);
            result.Value.Should().BeOfType<List<Customer>>();
            result.Value.Count.Should().Be(2);
        }

        [Theory]
        [InlineData("Customers could not be loaded")]
        public async void Get_WhenAnExceptionOccurs_ShouldReturnBadRequest(string exceptionMessage)
        {
            A.CallTo(() => _mediator.Send(A<GetCustomersQuery>._, default)).Throws(new Exception(exceptionMessage));

            var result = await _testee.Get();

            (result.Result as StatusCodeResult)?.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
            (result.Result as BadRequestObjectResult)?.Value.Should().Be(exceptionMessage);
        }

        [Fact]
        public async void Post_ShouldReturnCustomer()
        {
            var result = await _testee.Post(_createCustomerModel);

            (result.Result as StatusCodeResult)?.StatusCode.Should().Be((int) HttpStatusCode.OK);
            result.Value.Should().BeOfType<Customer>();
            result.Value.Id.Should().Be(_id);
        }

        [Fact]
        public async void Put_ShouldReturnCustomer()
        {
            var result = await _testee.Put(_updateCustomerModel);

            (result.Result as StatusCodeResult)?.StatusCode.Should().Be((int) HttpStatusCode.OK);
            result.Value.Should().BeOfType<Customer>();
            result.Value.Id.Should().Be(_id);
        }
    }
}