using App.Domain.Entities;

namespace App.Domain.Abstractions
{
    public interface IUserRepository
    {
        Task<User?> GetUserByIdAsync(int userId, CancellationToken cancellationToken = default);
        Task<User?> GetUserByUsernameAsync(string username, CancellationToken cancellationToken = default);
        Task<User?> GetUserByEmailAsync(string email, CancellationToken cancellationToken = default);

        Task AddUserAsync(User user, CancellationToken cancellationToken = default);
        Task DeleteUserAsync(User user, CancellationToken cancellationToken = default);
        Task UpdateUserAsync(User user, CancellationToken cancellationToken = default);
    }
}
