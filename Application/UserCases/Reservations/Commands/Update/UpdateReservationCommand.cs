using Application.Dto;
using Domain.Enuns;
using MediatR;

namespace Application.UserCases.Reservations.Commands.Update
{
    public class UpdateReservationCommand : IRequest<MessageResponseDto<object>>
    {
        public required int ReservationId { get; set; }
        public required DateTime Date { get; set; }
        public required EReservationStatus Status { get; set; }
    }
}
