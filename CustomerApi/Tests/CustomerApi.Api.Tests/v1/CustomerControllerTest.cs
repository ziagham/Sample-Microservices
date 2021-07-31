// using System;
// using Xunit;
// using Moq;

// namespace CustomerApi.Api.Tests.v1
// {
//     public class CustomerControllerTest
//     {
//         private readonly CustomerController _controller;
//         private readonly Mock<IMediator> _mediatorMoq;
        
//         public CustomerControllerTest(Parameters)
//         {
//             _mediatorMoq = new Mock<IMediator>();
//             _controller = new CustomerController(_mediatorMoq.Object);
//         }

//         [Fact]
//         public async void Task_Get_Return_OkResult()
//         {
//             //Arrange
//             var mock = new Mock<IGetDataRepository>();
//             mock.Setup(p => p.GetNameById(1)).Returns("Jignesh");
//             HomeController home = new HomeController(mock.Object);
//             string result = home.GetNameById(1);

//             //var controller = new PostController(repository);
//             //var postId = 2;

//             //Act
//             //var data = await controller.GetPost(postId);

//             //Assert
//             //Assert.IsType<OkObjectResult>(data);
//             Assert.AreEqual("Jignesh", result); 
//         }

//         [Fact]
//         public async void Task_Get_Return_NotFoundResult()
//         {

//         }
//     }
// }
