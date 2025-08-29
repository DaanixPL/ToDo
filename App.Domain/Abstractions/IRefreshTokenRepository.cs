using App.Domain.Entities;

namespace App.Domain.Abstractions
{
    public interface IRefreshTokenRepository
    {
        Task AddRefreshTokenAsync(RefreshToken refreshToken, CancellationToken cancellationToken = default);
        Task RemoveRefreshTokenAsync(RefreshToken refreshToken, CancellationToken cancellationToken = default);

        Task<RefreshToken?> GetByTokenAsync(string refreshToken, CancellationToken cancellationToken = default);
    }
}
