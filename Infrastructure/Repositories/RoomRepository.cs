using Domain.Abstractions;
using Domain.Entities;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class RoomRepository : IRoomRepository
    {
        protected readonly AppDbContext _context;
        public RoomRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddRoomAsync(Room room, CancellationToken cancellationToken)
        {
            await _context.Rooms.AddAsync(room);
        }

        public async Task<IEnumerable<Room>> GetRoomsAsync(CancellationToken cancellationToken)
        {
            var result = await _context.Rooms.ToListAsync(cancellationToken);
            return await _context.Rooms.ToListAsync(cancellationToken);
        }

        public async Task<Room> GetRoomByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _context.Rooms.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public void UpdateRoom(Room room)
        {
            _context.Rooms.Update(room);
        }
    }
}
