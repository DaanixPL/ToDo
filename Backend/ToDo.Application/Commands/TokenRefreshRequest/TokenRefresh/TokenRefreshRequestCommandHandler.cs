using ToDo.Application.Validators.Exceptions;
using ToDo.Domain.Abstractions;
using ToDo.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using ToDo.Application.Interfaces.Authentication;
using ToDo.Application.Responses;

namespace ToDo.Application.Commands.TokenRefreshRequest.TokenRefresh
{
    public class TokenRefreshRequestCommandHandler : IRequestHandler<TokenRefreshRequestCommand, RefreshTokenResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenGeneratorRepository _tokenGenerator;
        private readonly ILogger<TokenRefreshRequestCommandHandler> _logger;

        public TokenRefreshRequestCommandHandler(IUnitOfWork unitOfWork, ITokenGeneratorRepository tokenGenerator, ILogger<TokenRefreshRequestCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _tokenGenerator = tokenGenerator;
            _logger = logger;
        }

        public async Task<RefreshTokenResponse> Handle(TokenRefreshRequestCommand command, CancellationToken cancellationToken)
        {
            var oldRefreshToken = await _unitOfWork.RefreshTokens.GetByTokenAsync(command.RefreshToken, cancellationToken);

            if (oldRefreshToken == null || oldRefreshToken.IsRevoked || oldRefreshToken.ExpiresAt < DateTime.UtcNow)
            {
                _logger.LogWarning("Invalid or extinct refresh token {RefreshToken} used for token refresh", command.RefreshToken);
                throw new UnauthorizedAccessException("Invalid or extinct token.");
            }

            await _unitOfWork.RefreshTokens.RevokeRefreshTokenAsync(oldRefreshToken, cancellationToken);

            var user = await _unitOfWork.Users.GetUserByIdAsync(oldRefreshToken.UserId, cancellationToken);

            if (user == null)
            {
                _logger.LogError("User with ID {UserId} associated with refresh token not found", oldRefreshToken.UserId);
                throw new NotFoundException("User", oldRefreshToken.UserId);
            }

            var newAccessToken = _tokenGenerator.GenerateAccessToken(user);
            var newRefreshTokenString = _tokenGenerator.GenerateRefreshToken(user, Guid.NewGuid());

            var newRefreshToken = new RefreshToken
            {
                Token = newRefreshTokenString,
                User = user,
                UserId = user.Id,
                ExpiresAt = DateTime.UtcNow.AddHours(720)
            };

            _logger.LogInformation("Generated new access and refresh tokens for User ID {UserId}", user.Id);

            await _unitOfWork.RefreshTokens.AddRefreshTokenAsync(newRefreshToken, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new RefreshTokenResponse( newAccessToken, newRefreshTokenString);
        }
    }
}
