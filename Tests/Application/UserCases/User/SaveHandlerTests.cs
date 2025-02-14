using Application.UserCases.Users.Commands.Save;
using Domain.Abstractions;
using FluentAssertions;
using Moq;

namespace Tests.Application.UserCases.User
{
    public class SaveHandlerTests
    {
        private Mock<IUnitOfWork> _unitOfWork;
        private SaveUserHandler _handler;

        [SetUp]
        public void Setup()
        {
            _unitOfWork = new Mock<IUnitOfWork>();
            _handler = new SaveUserHandler(_unitOfWork.Object);
        }

        [Test]
        public async Task Handle_ShouldReturnFailureMessage_WhenUserAlreadyExists()
        {
            // Arrange
            var command = new SaveUserCommand
            {
                Name = "User Test",
                Email = "userExist@user.com",
                PhoneNumber = "999999999"
            };

            _unitOfWork.Setup(x => x.UserRepository.GetUserByEmailAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Domain.Entities.User());

            // Act
            var result = await _handler.Handle(command, new CancellationToken());

            // Assert
            result.Should().NotBeNull();
            result.Success.Should().BeFalse();
            result.Message.Should().Be("Email already exists");
        }

        [Test]
        public async Task Handle_ShouldThrowException_WhenUserRepositoryFails()
        {
            // Arrange
            var command = new SaveUserCommand
            {
                Name = "User Test",
                Email = "userExist@user.com",
                PhoneNumber = "999999999"
            };

            _unitOfWork.Setup(x => x.UserRepository.GetUserByEmailAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception("Error"));

            // Act
            var result = await _handler.Handle(command, new CancellationToken());

            // Assert
            result.Should().NotBeNull();
            result.Success.Should().BeFalse();
            result.Message.Should().Be("Error creating user: Error");
        }

        [Test]
        public async Task Handle_ShouldReturnSuccessMessage_WhenUserIsCreated()
        {
            // Arrange
            var command = new SaveUserCommand
            {
                Name = "User Test",
                Email = "UserOk@UserOK.com",
                PhoneNumber = "999999999"
            };

            _unitOfWork.Setup(x => x.UserRepository.GetUserByEmailAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((Domain.Entities.User)null);
            _unitOfWork.Setup(x => x.UserRepository.AddUserAsync(It.IsAny<Domain.Entities.User>(), It.IsAny<CancellationToken>()))
                .Verifiable();
            _unitOfWork.Setup(x => x.CommitAsync()).Verifiable();

            // Act
            var result = await _handler.Handle(command, new CancellationToken());

            // Assert
            result.Should().NotBeNull();
            result.Success.Should().BeTrue();
            result.Message.Should().Be("User created successfully");
        }
    }
}
