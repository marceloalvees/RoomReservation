using Application.Dto;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;

namespace Application.UserCases.Reservations.Commands.Update
{
    public class UpdateReservationHandler : IRequestHandler<UpdateReservationCommand, MessageResponseDto<object>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateReservationHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<MessageResponseDto<object>> Handle(UpdateReservationCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var reservation = await GetAndUpdateReservation(request, cancellationToken);
                await UpdateReservationAndCommit(reservation);

                return new MessageResponseDto<object>(true, "Reservation updated successfully");
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

        private async Task<Reservation> GetAndUpdateReservation(UpdateReservationCommand request, CancellationToken cancellationToken)
        {
            var reservation = await _unitOfWork.ReservationRepository.GetReservationByIdAsync(request.ReservationId, cancellationToken);

            if (reservation is null)
            {
                throw new ReservationException("Reservation not found");
            }

            reservation.Update(request.Date);
            if (request.Status == Domain.Enuns.EReservationStatus.Canceled)
            {
                reservation.Cancel();
            }

            return reservation;
        }

        private async Task UpdateReservationAndCommit(Reservation reservation)
        {
            _unitOfWork.ReservationRepository.UpdateReservation(reservation);
            await _unitOfWork.CommitAsync();
        }

        // Exceção personalizada para reservas
        public class ReservationException : Exception
        {
            public ReservationException(string message) : base(message)
            {
            }
        }
    }
}
