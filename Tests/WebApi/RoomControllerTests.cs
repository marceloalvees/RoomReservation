using Application.Dto;
using Application.UserCases.Rooms.Queries.GetAll;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebApi.Controllers;

namespace Tests.WebApi
{
    public class RoomControllerTests
    {
        private Mock<IMediator> _mediator;
        private RoomController _controller;

        [SetUp]
        public void Setup()
        {
            _mediator = new Mock<IMediator>();
            _controller = new RoomController(_mediator.Object);
        }

        [Test]
        public async Task GetRooms_ShouldReturnSuccess_WhenCommandIsValid()
        {
            // Arrange
            var query = new GetRoomsQuery();
            _mediator.Setup(x => x.Send(It.IsAny<GetRoomsQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new MessageResponseDto<List<GetRoomsResponse>>(true, "Sucess", null));
            // Act
            var result = await _controller.GetRooms(new CancellationToken());
            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
        }
    }
}
