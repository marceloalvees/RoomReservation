using Application.UserCases.Reservations.Commands.Update;
using Domain.Abstractions;
using FluentAssertions;
using Moq;

namespace Tests.Application.UserCases.Reservation
{
    public class UpdateReservationTests
    {
        private Mock<IUnitOfWork> _unitOfWork;
        private UpdateReservationHandler _handler;

        [SetUp]
        public void Setup()
        {
            _unitOfWork = new Mock<IUnitOfWork>();
            _handler = new UpdateReservationHandler(_unitOfWork.Object);
        }

        [Test]
        public async Task Handle_ShouldReturnFailureMessage_WhenReservationDoesNotExists()
        {
            // Arrange
            var command = new UpdateReservationCommand
            {
                ReservationId = 1,
                Date = DateTime.Now,
                Status = Domain.Enuns.EReservationStatus.Canceled
            };
            _unitOfWork.Setup(x => x.ReservationRepository.GetReservationByIdAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((Domain.Entities.Reservation)null);
            
            // Act
            var result = await _handler.Handle(command, new CancellationToken());
            
            // Assert
            result.Should().NotBeNull();
            result.Success.Should().BeFalse();
            result.Message.Should().Be("Reservation not found");
        }

        [Test]
        public async Task Handle_ShouldThrowException_WhenUpdateReservationFails()
        {
            // Arrange
            var command = new UpdateReservationCommand
            {
                ReservationId = 1,
                Date = DateTime.Now.AddDays(1),
                Status = Domain.Enuns.EReservationStatus.Canceled
            };
            _unitOfWork.Setup(x => x.ReservationRepository.GetReservationByIdAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Domain.Entities.Reservation());
            //_unitOfWork.Setup(x=> x.ReservationRepository.U)
            _unitOfWork.Setup(x => x.CommitAsync())
                .ThrowsAsync(new Exception("Error"));

            // Act
            var result = await _handler.Handle(command, new CancellationToken());

            // Assert
            result.Should().NotBeNull();
            result.Success.Should().BeFalse();
            result.Message.Should().Be("Error: Error");
        }

        [Test]
        public async Task Handle_ShouldReturnSuccessMessage_WhenReservationIsUpdated()
        {
            // Arrange
            var command = new UpdateReservationCommand
            {
                ReservationId = 1,
                Date = DateTime.Now.AddDays(1),
                Status = Domain.Enuns.EReservationStatus.Canceled
            };
            _unitOfWork.Setup(x => x.ReservationRepository.GetReservationByIdAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Domain.Entities.Reservation());
            // Act
            var result = await _handler.Handle(command, new CancellationToken());
            // Assert
            result.Should().NotBeNull();
            result.Success.Should().BeTrue();
            result.Message.Should().Be("Reservation updated successfully");
        }
    }
}
