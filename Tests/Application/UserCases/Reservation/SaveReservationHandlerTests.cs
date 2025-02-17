using Application.UserCases.Reservations.Commands.Save;
using Domain.Abstractions;
using FluentAssertions;
using Moq;

namespace Tests.Application.UserCases.Reservation
{
    public class SaveReservationHandlerTests
    {
        private Mock<IUnitOfWork> _unitOfWork;
        private Mock<IEmailService> _emailService;
        private SaveReservationHandler _handler;
        [SetUp]
        public void Setup()
        {
            _unitOfWork = new Mock<IUnitOfWork>();
            _emailService = new Mock<IEmailService>();
            _handler = new SaveReservationHandler(_unitOfWork.Object, _emailService.Object);
        }
        [Test]
        public async Task Handle_ShouldReturnSuccessMessage_WhenReservationIsCreated()
        {
            // Arrange
            var command = new SaveReservationCommand
            {
                RoomId = 2,
                UsertEmail = "user@user.com",
                Date = DateTime.Now.AddDays(1)
            };
            var user = new Domain.Entities.User("user", "user@user.com", "12345648");
            user.Id = 1;
            var room = new Domain.Entities.Room("sala1", 10, "sede");
            room.Id = 2;

            _unitOfWork.Setup(x => x.UserRepository.GetUserByEmailAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(user);
            _unitOfWork.Setup(x => x.RoomRepository.GetRoomByIdAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(room);
            _unitOfWork.Setup(x => x.ReservationRepository.AddReservationAsync(It.IsAny<Domain.Entities.Reservation>(), It.IsAny<CancellationToken>()))
                .Callback<Domain.Entities.Reservation, CancellationToken>((reservation, cancellationToken) => reservation.Id = 1);
            _unitOfWork.Setup(x => x.CommitAsync()).Callback(() => { });

            // Act
            var result = await _handler.Handle(command, new CancellationToken());

            // Assert
            result.Should().NotBeNull();
            result.Success.Should().BeTrue();
            result.Message.Should().Be("Reservation created successfully");
        }

        [Test]
        public async Task Handle_ShouldReturnFailureMessage_WhenUserDoesNotExists()
        {
            // Arrange
            var command = new SaveReservationCommand
            {
                RoomId = 2,
                UsertEmail = "user@user.com",
                Date = DateTime.Now.AddDays(1)
            };

            _unitOfWork.Setup(x => x.UserRepository.GetUserByEmailAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((Domain.Entities.User)null);

            _unitOfWork.Setup(x => x.RoomRepository.GetRoomByIdAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Domain.Entities.Room());

            // Act
            var result = await _handler.Handle(command, new CancellationToken());

            // Assert
            result.Should().NotBeNull();
            result.Success.Should().BeFalse();
            result.Message.Should().Be("User not found");
        }

        [Test]
        public async Task Handle_ShouldReturnFailureMessage_WhenRoomDoesNotExists()
        {
            // Arrange
            var command = new SaveReservationCommand
            {
                RoomId = 2,
                UsertEmail = "user@user.com",
                Date = DateTime.Now.AddDays(1)
            };

            var user = new Domain.Entities.User("user", "user@user.com", "12345648");
            user.Id = 1;

            var room = new Domain.Entities.Room("sala1", 10, "sede");
            room.Id = 2;

            _unitOfWork.Setup(x => x.UserRepository.GetUserByEmailAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(user);

            _unitOfWork.Setup(x => x.RoomRepository.GetRoomByIdAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .Callback<int, CancellationToken>((id, cancellationToken) => { });

            // Act
            var result = await _handler.Handle(command, new CancellationToken());

            // Assert
            result.Should().NotBeNull();
            result.Success.Should().BeFalse();
            result.Message.Should().Be("Room not found");
        }
    }
}
