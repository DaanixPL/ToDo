namespace App.Domain.Abstractions
{
    public interface IUnitOfWork : IDisposable
    {
        public IUserRepository Users { get; }
        public IRefreshTokenRepository RefreshTokens { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
