using Application.Dto;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;

namespace Application.UserCases.Users.Commands.Save
{
    public class SaveUserHandler : IRequestHandler<SaveUserCommand, MessageResponseDto<object>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public SaveUserHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<MessageResponseDto<object>> Handle(SaveUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if(await _unitOfWork.UserRepository.GetUserByEmailAsync(request.Email, cancellationToken) != null)
                {
                    return new MessageResponseDto<object>(false, "Email already exists");
                }

                var user = new User(request.Name, request.Email, request.PhoneNumber);
                await _unitOfWork.UserRepository.AddUserAsync(user, cancellationToken);
                await _unitOfWork.CommitAsync();

                return new MessageResponseDto<object>(true, "User created successfully");
            }
            catch (Exception ex) 
            {
                return new MessageResponseDto<object>(false, $"Error creating user: {ex.Message}");
            }
        }
    }
}
