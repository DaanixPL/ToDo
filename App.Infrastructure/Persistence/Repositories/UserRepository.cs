using ToDo.Domain.Abstractions;
using ToDo.Domain.Entities;
using App.Infrastructure.Persistence.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace App.Infrastructure.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _dbContext;

        public UserRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User?> GetUserByIdAsync(int userId, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Users.FindAsync(new object[] { userId }, cancellationToken);
        }
        public async Task<User?> GetUserByUsernameAsync(string username, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(u => u.Username == username, cancellationToken);
        }
        public async Task<User?> GetUserByEmailAsync(string email, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
        }


        public async Task AddUserAsync(User user, CancellationToken cancellationToken = default)
        {
            await _dbContext.AddAsync(user, cancellationToken);
        }
        public Task DeleteUserAsync(User user, CancellationToken cancellationToken = default)
        {
            _dbContext.Users.Remove(user);
            return Task.CompletedTask;
        }
        public Task UpdateUserAsync(User user, CancellationToken cancellationToken = default)
        {
            _dbContext.Users.Update(user);
            return Task.CompletedTask;
        }
    }
}
