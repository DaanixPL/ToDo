using ToDo.Domain.Abstractions;
using ToDo.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using ToDo.Infrastructure.Persistence.Data.DbContexts;

namespace ToDo.Infrastructure.Persistence.Repositories
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

            if (user == null)
            {
                throw new InvalidOperationException($"User with ID {refreshToken.UserId} not found");
            }

            user.RefreshTokens.Add(refreshToken);
        }
        public async Task RemoveRefreshTokenAsync(RefreshToken refreshToken, CancellationToken cancellationToken = default)
        {
            var tokenToRemove = await _dbContext.RefreshTokens
                    .FirstOrDefaultAsync(t => t.Token == refreshToken.Token, cancellationToken);

            if (tokenToRemove != null)
            {
                _dbContext.RefreshTokens.Remove(tokenToRemove);
            }
        }
        public async Task RevokeRefreshTokenAsync(RefreshToken refreshToken, CancellationToken cancellationToken = default)
        {
            var tokenToRevoke = await _dbContext.RefreshTokens
                    .FirstOrDefaultAsync(t => t.Token == refreshToken.Token, cancellationToken);

            if (tokenToRevoke == null)
            {
                throw new InvalidOperationException($"Refresh token not found");
            }

            tokenToRevoke.IsRevoked = true;
        }

        public async Task<RefreshToken?> GetByTokenAsync(string refreshToken, CancellationToken cancellationToken = default)
        {
            return await _dbContext.RefreshTokens
                .FirstOrDefaultAsync(t => t.Token == refreshToken, cancellationToken);
        }
    }
}
