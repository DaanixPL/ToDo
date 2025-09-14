namespace App.Domain.Abstractions
{
    public interface IUnitOfWork : IDisposable
    {
        public IUserRepository Users { get; }
        public IRefreshTokenRepository RefreshTokens { get; }
        public ITodoItemRepository TodoItems { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
