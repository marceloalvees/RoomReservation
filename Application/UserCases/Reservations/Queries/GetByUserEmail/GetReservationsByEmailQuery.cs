using System.ComponentModel.DataAnnotations;
using Application.Dto;
using MediatR;

namespace Application.UserCases.Reservations.Queries.GetByUserEmail
{
    public class GetReservationsByEmailQuery : IRequest<MessageResponseDto<IEnumerable<GetReservationsByEmailResponse>>>
    {
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public required string Email { get; set; }
    }
}
