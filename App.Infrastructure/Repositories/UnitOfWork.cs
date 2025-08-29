using App.Domain.Abstractions;
using App.Infrastructure.Context;

namespace App.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private readonly IUserRepository _userRepository;
        private readonly IRefreshTokenRepository _refreshTokenRepository;


        private bool _disposed = false;


        public UnitOfWork(AppDbContext context, IUserRepository userRepository, IRefreshTokenRepository refreshTokenRepository)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _refreshTokenRepository = refreshTokenRepository ?? throw new ArgumentNullException(nameof(refreshTokenRepository));
        }

        public IUserRepository Users => _userRepository;
        public IRefreshTokenRepository RefreshTokens => _refreshTokenRepository;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) 
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }

    }
}
