using Application.Dto;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;

namespace Application.UserCases.Reservations.Commands.Save
{
    public class SaveReservationHandler : IRequestHandler<SaveReservationCommand, MessageResponseDto<object>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailService _emailService;

        public SaveReservationHandler(IUnitOfWork unitOfWork, IEmailService emailService)
        {
            _unitOfWork = unitOfWork;
            _emailService = emailService;
        }
        public async Task<MessageResponseDto<object>> Handle(SaveReservationCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var (reservation, email) = await ValidateAndCreateReservation(request, cancellationToken);

                await AddReservationAndSendConfirmationEmail(reservation, email, cancellationToken);

                return new MessageResponseDto<object>(true, "Reservation created successfully");
            }
            catch (ReservationException ex)
            {
                return new MessageResponseDto<object>(false, ex.Message);
            }
            catch (Exception ex)
            {
                return new MessageResponseDto<object>(false, $"Error: {ex.Message}");
            }
        }

        private async Task<(Reservation reservation, string email)> ValidateAndCreateReservation(SaveReservationCommand request, CancellationToken cancellationToken)
        {
            var (user, room) = await GetUserAndRoom(request, cancellationToken);

            ValidateUserAndRoom(user, room);
            await ValidateRoomAvailability(request.RoomId, request.Date, cancellationToken);

            return (new Reservation(room, user, request.Date), user.Email);
        }

        private async Task<(User user, Room room)> GetUserAndRoom(SaveReservationCommand request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.UserRepository.GetUserByEmailAsync(request.UsertEmail, cancellationToken);
            var room = await _unitOfWork.RoomRepository.GetRoomByIdAsync(request.RoomId, cancellationToken);

            return (user, room);
        }

        private void ValidateUserAndRoom(User user, Room room)
        {
            if (user is null)
            {
                throw new ReservationException("User not found");
            }

            if (room is null)
            {
                throw new ReservationException("Room not found");
            }
        }

        private async Task ValidateRoomAvailability(int roomId, DateTime date, CancellationToken cancellationToken)
        {
            if (await _unitOfWork.ReservationRepository.IsRoomReservedAsync(roomId, date, cancellationToken))
            {
                throw new ReservationException("Room already reserved");
            }
        }

        private async Task AddReservationAndSendConfirmationEmail(Reservation reservation, string email, CancellationToken cancellationToken)
        {
            await _unitOfWork.ReservationRepository.AddReservationAsync(reservation, cancellationToken);
            await _emailService.SendEmailAsync(email, "Reservation Created", "Your reservation was created successfully");
            await _unitOfWork.CommitAsync();
        }

        public class ReservationException : Exception
        {
            public ReservationException(string message) : base(message)
            {
            }
        }

    }
}
