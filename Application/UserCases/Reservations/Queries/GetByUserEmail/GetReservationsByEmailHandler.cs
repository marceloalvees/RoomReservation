using Application.Dto;
using AutoMapper;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;

namespace Application.UserCases.Reservations.Queries.GetByUserEmail
{
    public class GetReservationsByEmailHandler : IRequestHandler<GetReservationsByEmailQuery, MessageResponseDto<IEnumerable<GetReservationsByEmailResponse>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetReservationsByEmailHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<MessageResponseDto<IEnumerable<GetReservationsByEmailResponse>>> Handle(GetReservationsByEmailQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _unitOfWork.UserRepository.GetUserByEmailAsync(request.Email, cancellationToken);
                if (user == null) 
                {
                    return new MessageResponseDto<IEnumerable<GetReservationsByEmailResponse>>(false, "User not found");
                }

                 var reservations =await _unitOfWork.ReservationRepository.GetReservationsByUserIdAsync(user.Id, cancellationToken);
                 var response = await MapReservations(reservations, cancellationToken);
                return new MessageResponseDto<IEnumerable<GetReservationsByEmailResponse>>(true, string.Empty, response);
            }
            catch (Exception ex)
            {
                return new MessageResponseDto<IEnumerable<GetReservationsByEmailResponse>>(false, $"Error: {ex.Message}");
            }
        }

        private async Task<IEnumerable<GetReservationsByEmailResponse>> MapReservations(IEnumerable<Reservation> reservations, CancellationToken cancellationToken)
        {
            var listReservations = new List<GetReservationsByEmailResponse>();
            foreach(var reservation in reservations)
            {
                var room = await _unitOfWork.RoomRepository.GetRoomByIdAsync(reservation.RoomId, cancellationToken);
                listReservations.Add(new GetReservationsByEmailResponse
                {
                    Id = reservation.Id,
                    Date = reservation.Date,
                    RoomName = room.Name,
                    Capacity = room.Capacity,
                    Status = reservation.Status
                });

            }
            return listReservations;
        }
    }
}
