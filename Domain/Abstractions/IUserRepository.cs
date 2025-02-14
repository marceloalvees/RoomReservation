using Domain.Entities;

namespace Domain.Abstractions
{
    public interface IUserRepository
    {
        Task<User> GetUserByEmailAsync(string email, CancellationToken cancellationToken);
        Task AddUserAsync(User user, CancellationToken cancellationToken);
        void UpdateUser(User user);
        void DeleteUser(User user);
        Task<User> GetUserByIdAsync(int id, CancellationToken cancellationToken);
    }
}
