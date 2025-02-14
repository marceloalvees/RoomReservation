using Domain.Entities;

namespace Domain.Abstractions
{
    public interface IRoomRepository
    {
        Task<IEnumerable<Room>> GetRoomsAsync(CancellationToken cancellationToken);
        Task<Room> GetRoomByIdAsync(int id, CancellationToken cancellationToken );
        Task AddRoomAsync(Room room, CancellationToken cancellationToken);
        void UpdateRoom(Room room);
    }
}
