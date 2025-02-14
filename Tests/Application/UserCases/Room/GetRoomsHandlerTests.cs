using Application.UserCases.Rooms.Queries.GetAll;
using AutoMapper;
using Domain.Abstractions;
using FluentAssertions;
using Moq;

namespace Tests.Application.UserCases.Room
{
    public class GetRoomsHandlerTests
    {
        private Mock<IUnitOfWork> _unitOfWork;
        private Mock<IMapper> _mapper;
        private GetRoomsHandler _handler;

        [SetUp]
        public void Setup()
        {
            _unitOfWork = new Mock<IUnitOfWork>();
            _mapper = new Mock<IMapper>();
            _handler = new GetRoomsHandler(_unitOfWork.Object, _mapper.Object);
        }

        [Test]
        public async Task Handle_ShouldReturnRooms_WhenRoomsExists()
        {
            // Arrange
            var query = new GetRoomsQuery();
            var rooms = new List<Domain.Entities.Room>
            {
                new Domain.Entities.Room("Room 1", 10, "Sede"),
                new Domain.Entities.Room("Room 2", 20, "sede2")
            };
            var roomsResponse = new List<GetRoomsResponse>
            {
                new GetRoomsResponse { Id = 1, Name = "Room 1", Capacity = 10, Location = "Sede" },
                new GetRoomsResponse {Id= 2, Name = "Room 2", Capacity = 20, Location= "Sede2" }
            };
            _unitOfWork.Setup(x => x.RoomRepository.GetRoomsAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(rooms);
            _mapper.Setup(x => x.Map<IEnumerable<GetRoomsResponse>>(rooms))
                .Returns(roomsResponse);
            // Act
            var result = await _handler.Handle(query, new CancellationToken());
            // Assert
            result.Should().NotBeNull();
            result.Success.Should().BeTrue();
            result.Data.Should().NotBeNull();
            result.Data.Should().BeEquivalentTo(roomsResponse);
        }

        [Test]
        public async Task Handle_ShouldThrowException_WhenRoomRepositoryFails()
        {
            // Arrange
            var query = new GetRoomsQuery();
            _unitOfWork.Setup(x => x.RoomRepository.GetRoomsAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception("REPOSITORY"));
            // Act
            var result = await _handler.Handle(query, new CancellationToken());
            // Assert
            result.Should().NotBeNull();
            result.Success.Should().BeFalse();
            result.Message.Should().Be("Error: REPOSITORY");
        }
    }
}
