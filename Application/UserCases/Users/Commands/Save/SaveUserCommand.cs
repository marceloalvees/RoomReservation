using Application.Dto;
using MediatR;

namespace Application.UserCases.Users.Commands.Save
{
    public class SaveUserCommand : IRequest<MessageResponseDto<object>>
    {
        public required string Name { get; init; }
        public required string Email { get; init; }
        public required string PhoneNumber { get; init; }
    }
}
