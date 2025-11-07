using App.Application.Validators.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Logging;
using ToDo.Domain.Abstractions;

namespace App.Application.Commands.TokenRefreshRequest.RevokeRefreshToken
{
    public class RevokeRefreshTokenCommandHandler : IRequestHandler<RevokeRefreshTokenCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<RevokeRefreshTokenCommandHandler> _logger;
        public RevokeRefreshTokenCommandHandler(IUnitOfWork unitOfWork, ILogger<RevokeRefreshTokenCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        public async Task<Unit> Handle(RevokeRefreshTokenCommand command, CancellationToken cancellationToken)
        {
            var refreshToken = await _unitOfWork.RefreshTokens.GetByTokenAsync(command.RefreshToken, cancellationToken);
            if (refreshToken == null)
            {
                _logger.LogWarning("Refresh token {RefreshToken} not found for revocation", command.RefreshToken);
                throw new NotFoundException("Refresh token", command.RefreshToken);
            }
            else if (refreshToken.IsRevoked)
            {
                _logger.LogWarning("Attempt to revoke already revoked refresh token {RefreshToken}", command.RefreshToken);
                throw new ForbiddenException("Refresh token", command.RefreshToken);
            }

            _logger.LogInformation("Revoking refresh token {RefreshToken}", command.RefreshToken);

            await _unitOfWork.RefreshTokens.RevokeRefreshTokenAsync(refreshToken, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
