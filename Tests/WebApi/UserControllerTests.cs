using Application.Dto;
using Application.UserCases.Users.Commands.Save;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebApi.Controllers;

namespace Tests.WebApi
{
    public class UserControllerTests
    {
        private Mock<IMediator> _mediator;
        private UserController _controller;

        [SetUp]
        public void Setup()
        {
            _mediator = new Mock<IMediator>();
            _controller = new UserController(_mediator.Object);
        }

        [Test]
        public async Task Save_ShouldReturnSuccess_WhenCommandIsValid()
        {
            // Arrange
            var command = new SaveUserCommand
            {
                Name = "User Test",
                Email = "user@user.com",
                PhoneNumber = "999999999"

            };

            _mediator.Setup(x => x.Send(It.IsAny<SaveUserCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new MessageResponseDto<object>(true, "Sucess", null));

            // Act
            var result = await _controller.Post(command);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
        }
    }
}
