using Application.Dto;
using MediatR;

namespace Application.UserCases.Reservations.Commands.Save
{
    public class SaveReservationCommand : IRequest<MessageResponseDto<object>>
    {
        public required int RoomId { get; set; }
        public required string UsertEmail { get; set; }
        public required DateTime Date { get; set; }
    }
}
