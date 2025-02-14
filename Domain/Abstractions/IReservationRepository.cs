
using Domain.Entities;

namespace Domain.Abstractions
{
    public interface IReservationRepository
    {
        Task<IEnumerable<Reservation>> GetReservationsAsync(CancellationToken cancellationToken);
        Task<Reservation> GetReservationByRoomIdAsync(int roomId, CancellationToken cancellationToken);
        Task<IEnumerable<Reservation>> GetReservationsByUserIdAsync(int userId, CancellationToken cancellationToken);
        Task<Reservation> GetReservationByIdAsync(int reservationId, CancellationToken cancellationToken);
        Task<bool> IsRoomReservedAsync(int roomId, DateTime startDate, CancellationToken cancellationToken);
        Task AddReservationAsync(Reservation reservation, CancellationToken cancellationToken);
        void UpdateReservation(Reservation reservation);
    }
}
