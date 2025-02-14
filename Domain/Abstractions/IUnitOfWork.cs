namespace Domain.Abstractions
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository { get; }
        IRoomRepository RoomRepository { get; }
        IReservationRepository ReservationRepository { get; }
        Task CommitAsync();
    }
}
