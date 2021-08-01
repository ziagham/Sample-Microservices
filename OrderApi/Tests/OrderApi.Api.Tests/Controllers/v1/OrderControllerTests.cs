using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using FakeItEasy;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderApi.Services.v1.Features.Command.CreateOrder;
using OrderApi.Services.v1.Features.Command.PayOrder;
using OrderApi.Services.v1.Features.Query.GetOrderById;
using OrderApi.Services.v1.Features.Query.GetPaidOrder;
using OrderApi.Domain.AggregatesModel.OrderAggregate;
using OrderApi.Api.Controllers.v1;
using Xunit;

namespace OrderApi.Api.Tests.Controllers.v1
{
    public class OrderControllerTests
    {
        private readonly OrderController _testee;
        private readonly CreateOrderCommand _createOrderCommand;
        private readonly Guid _id = Guid.Parse("35296ce1-e20f-4dc6-83c8-25b9152995e0");
        private readonly IMediator _mediator;

        public OrderControllerTests()
        {
            _mediator = A.Fake<IMediator>();
            _testee = new OrderController(_mediator);

            _createOrderCommand = new CreateOrderCommand
            {
                CustomerFullName = "Darth Vader",
                CustomerGuid = Guid.Parse("f1eef36b-d525-416b-acc5-effa50389db1")
            };
            var orders = new List<Order>
            {
                new Order
                {
                    CustomerFullName = "Darth Vader",
                    CustomerGuid = Guid.Parse("f1eef36b-d525-416b-acc5-effa50389db1"),
                    Id = _id,
                    OrderState = 1
                },
                new Order
                {
                    CustomerFullName = "Son Goku",
                    CustomerGuid = Guid.Parse("29c4c5f6-e907-4956-9491-659cb838d41e"),
                    Id = Guid.Parse("270b0d0f-cfde-4846-a67d-098166c333a1"),
                    OrderState = 2
                }
            };
            
            A.CallTo(() => _mediator.Send(A<CreateOrderCommand>._, A<CancellationToken>._)).Returns(orders.First());
            A.CallTo(() => _mediator.Send(A<PayOrderCommand>._, A<CancellationToken>._)).Returns(orders.Last());
            A.CallTo(() => _mediator.Send(A<GetOrderByIdQuery>._, A<CancellationToken>._)).Returns(orders.Last());
            A.CallTo(() => _mediator.Send(A<GetPaidOrderQuery>._, A<CancellationToken>._)).Returns(orders.FindAll(x => x.OrderState == 2));
        }

        [Theory]
        [InlineData("CreateOrderAsync: order is null")]
        public async void Post_WhenAnExceptionOccurs_ShouldReturnBadRequest(string exceptionMessage)
        {
            A.CallTo(() => _mediator.Send(A<CreateOrderCommand>._, default)).Throws(new ArgumentException(exceptionMessage));

            var result = await _testee.Post(_createOrderCommand);

            (result.Result as StatusCodeResult)?.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
            (result.Result as BadRequestObjectResult)?.Value.Should().Be(exceptionMessage);
        }

        [Theory]
        [InlineData("UpdateOrderAsync: order is null")]
        [InlineData("No order found this id")]
        public async void Pay_WhenAnExceptionOccurs_ShouldReturnBadRequest(string exceptionMessage)
        {
            A.CallTo(() => _mediator.Send(A<PayOrderCommand>._, default)).Throws(new Exception(exceptionMessage));

            var result = await _testee.Pay(_id);

            (result.Result as StatusCodeResult)?.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
            (result.Result as BadRequestObjectResult)?.Value.Should().Be(exceptionMessage);
        }

        [Fact]
        public async void Post_ShouldReturnOrder()
        {
            var result = await _testee.Post(_createOrderCommand);

            (result.Result as StatusCodeResult)?.StatusCode.Should().Be((int)HttpStatusCode.OK);
            result.Value.Should().BeOfType<Order>();
            result.Value.Id.Should().Be(_id);
        }

        [Fact]
        public async void Get_ShouldReturnListOfOrders()
        {
            var result = await _testee.Get();

            (result.Result as StatusCodeResult)?.StatusCode.Should().Be((int)HttpStatusCode.OK);
            result.Value.Should().BeOfType<List<Order>>();
            result.Value.Count.Should().Be(1);
        }

        [Fact]
        public async void Pay_WhenAnExceptionOccurs_ShouldReturnInternalServerError()
        {
            A.CallTo(() => _mediator.Send(A<PayOrderCommand>._, A<CancellationToken>._)).Throws(new Exception());

            var result = await _testee.Pay(_id);

            (result.Result as StatusCodeResult)?.StatusCode.Should().Be((int)HttpStatusCode.InternalServerError);
        }

        [Fact]
        public async void Get_WhenNoOrdersWereFound_ShouldReturnEmptyList()
        {
            A.CallTo(() => _mediator.Send(A<GetPaidOrderQuery>._, A<CancellationToken>._)).Returns(new List<Order>());

            var result = await _testee.Get();

            (result.Result as StatusCodeResult)?.StatusCode.Should().Be((int)HttpStatusCode.OK);
            result.Value.Should().BeOfType<List<Order>>();
            result.Value.Count.Should().Be(0);
        }

        [Fact]
        public async void Pay_ShouldReturnOrder()
        {
            var result = await _testee.Pay(_id);

            (result.Result as StatusCodeResult)?.StatusCode.Should().Be((int)HttpStatusCode.OK);
            result.Value.Should().BeOfType<Order>();
            result.Value.OrderState.Should().Be(2);
        }
    }
}