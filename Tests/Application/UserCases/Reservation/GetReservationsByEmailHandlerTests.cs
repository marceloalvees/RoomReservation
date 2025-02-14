using Application.UserCases.Reservations.Queries.GetByUserEmail;
using Domain.Abstractions;
using FluentAssertions;
using Moq;

namespace Tests.Application.UserCases.Reservation
{
    public class GetReservationsByEmailHandlerTests
    {
        private Mock<IUnitOfWork> _unitOfWork;
        private GetReservationsByEmailHandler _handler;

        [SetUp]
        public void Setup()
        {
            _unitOfWork = new Mock<IUnitOfWork>();
            _handler = new GetReservationsByEmailHandler(_unitOfWork.Object);
        }

        [Test]
        public async Task Handle_ShouldReturnFailureMessage_WhenUserDoesNotExists()
        {
            // Arrange
            var query = new GetReservationsByEmailQuery
            {
                Email = "user@user.com"
            };

            _unitOfWork.Setup(x => x.UserRepository.GetUserByEmailAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((Domain.Entities.User)null);

            // Act
            var result = await _handler.Handle(query, new CancellationToken());

            // Assert
            result.Should().NotBeNull();
            result.Success.Should().BeFalse();
            result.Message.Should().Be("User not found");
        }

        [Test]
        public async Task Handle_ShouldThrowException_WhenReservationsRepositoryFails()
        {
            // Arrange
            var query = new GetReservationsByEmailQuery
            {
                Email = "user@user.com"
            };

            _unitOfWork.Setup(x => x.UserRepository.GetUserByEmailAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Domain.Entities.User());

            _unitOfWork.Setup(x => x.ReservationRepository.GetReservationsByUserIdAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception("Error"));

            // Act
            var result = await _handler.Handle(query, new CancellationToken());

            // Assert
            result.Should().NotBeNull();
            result.Success.Should().BeFalse();
        }
    }
}
