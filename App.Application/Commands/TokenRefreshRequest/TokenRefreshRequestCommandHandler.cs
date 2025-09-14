using App.Application.Interfaces.Authentication;
using App.Application.Responses;
using App.Application.Validators.Exceptions;
using App.Domain.Abstractions;
using App.Domain.Entities;
using MediatR;

namespace App.Application.Commands.TokenRefreshRequest
{
    public class TokenRefreshRequestCommandHandler : IRequestHandler<TokenRefreshRequestCommand, RefreshTokenResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenGeneratorRepository _tokenGenerator;

        public TokenRefreshRequestCommandHandler(IUnitOfWork unitOfWork, ITokenGeneratorRepository tokenGenerator)
        {
            _unitOfWork = unitOfWork;
            _tokenGenerator = tokenGenerator;
        }

        public async Task<RefreshTokenResponse> Handle(TokenRefreshRequestCommand command, CancellationToken cancellationToken)
        {
            var oldRefreshToken = await _unitOfWork.RefreshTokens.GetByTokenAsync(command.RefreshToken, cancellationToken);

            if (oldRefreshToken == null || oldRefreshToken.IsRevoked || oldRefreshToken.ExpiresAt < DateTime.UtcNow)
            {
                throw new UnauthorizedAccessException("Invalid or extinct token.");
            }

            await _unitOfWork.RefreshTokens.RemoveRefreshTokenAsync(oldRefreshToken, cancellationToken);

            var user = await _unitOfWork.Users.GetUserByIdAsync(oldRefreshToken.UserId, cancellationToken);

            if (user == null)
            {
                throw new UnauthorizedAccessException("Invalid credentials.");
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

            await _unitOfWork.RefreshTokens.AddRefreshTokenAsync(newRefreshToken, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new RefreshTokenResponse( newAccessToken, newRefreshTokenString);
        }
    }
}
