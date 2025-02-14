using Domain.Abstractions;
using Infrastructure.Context;

namespace Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        public IUserRepository? _userRepository;
        public IRoomRepository? _roomRepository;
        public IReservationRepository? _reservationRepository;
        private readonly AppDbContext _context;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public IUserRepository UserRepository 
        {
            get
            {
                return _userRepository = _userRepository ?? new UserRepository(_context);
            }
        }

        public IRoomRepository RoomRepository
        {
            get
            {
                return _roomRepository = _roomRepository ?? new RoomRepository(_context);
            }
        }

        public IReservationRepository ReservationRepository
        {
            get
            {
                return _reservationRepository = _reservationRepository ?? new ReservationRepository(_context);
            }
        }
        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
    
}
