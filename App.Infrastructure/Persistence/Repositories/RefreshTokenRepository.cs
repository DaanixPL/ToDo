using ToDo.Domain.Abstractions;
using ToDo.Domain.Entities;
using App.Infrastructure.Persistence.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace App.Infrastructure.Persistence.Repositories
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly AppDbContext _dbContext;

        public RefreshTokenRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddRefreshTokenAsync(RefreshToken refreshToken, CancellationToken cancellationToken = default)
        {
            var user = await _dbContext.Users
                .Include(u => u.RefreshTokens)
                .FirstOrDefaultAsync(u => u.Id == refreshToken.UserId, cancellationToken);

            user.RefreshTokens.Add(refreshToken);
        }
        public async Task RemoveRefreshTokenAsync(RefreshToken refreshToken, CancellationToken cancellationToken = default)
        {
            var tokenToRemove = await _dbContext.RefreshTokens
                    .FirstOrDefaultAsync(t => t.Token == refreshToken.Token);

            _dbContext.RefreshTokens.Remove(tokenToRemove);
        }

        public async Task<RefreshToken?> GetByTokenAsync(string refreshToken, CancellationToken cancellationToken = default)
        {
            return await _dbContext.RefreshTokens
                .FirstOrDefaultAsync(t => t.Token == refreshToken, cancellationToken);
        }
    }
}
