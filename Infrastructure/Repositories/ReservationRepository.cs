using Domain.Abstractions;
using Domain.Entities;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ReservationRepository : IReservationRepository
    {
        protected readonly AppDbContext _context;
        public ReservationRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddReservationAsync(Reservation reservation, CancellationToken cancellationToken)
        {
            await _context.Reservations.AddAsync(reservation);
        }

        public async Task<IEnumerable<Reservation>> GetReservationsAsync(CancellationToken cancellationToken)
        {
            return await _context.Reservations.ToListAsync(cancellationToken);
        }

        public async Task<Reservation> GetReservationByRoomIdAsync(int roomId, CancellationToken cancellationToken)
        {
            return await _context.Reservations.FirstOrDefaultAsync(x => x.RoomId == roomId, cancellationToken);
        }

        public async Task<Reservation> GetReservationByIdAsync(int reservationId, CancellationToken cancellationToken)
        {
            return await _context.Reservations.FirstOrDefaultAsync(x => x.Id == reservationId, cancellationToken);
        }

        public async Task<IEnumerable<Reservation>> GetReservationsByUserIdAsync(int userId, CancellationToken cancellationToken)
        {
            return await _context.Reservations.Where(x => x.UserId == userId).ToListAsync(cancellationToken);
        }

        public async Task<bool> IsRoomReservedAsync(int roomId, DateTime startDate, CancellationToken cancellationToken)
        {
            return await _context.Reservations.AnyAsync(x => x.RoomId == roomId && x.Date == startDate, cancellationToken);
        }

        public void UpdateReservation(Reservation reservation)
        {
            _context.Reservations.Update(reservation);
        }
    }
}
