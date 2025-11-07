using App.Application.Validators.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using ToDo.Domain.Abstractions;

namespace App.Application.Commands.TokenRefreshRequest.RevokeRefreshToken
{
    public class RevokeRefreshTokenCommandHandler : IRequestHandler<RevokeRefreshTokenCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        public RevokeRefreshTokenCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Unit> Handle(RevokeRefreshTokenCommand command, CancellationToken cancellationToken)
        {
            var refreshToken = await _unitOfWork.RefreshTokens.GetByTokenAsync(command.RefreshToken, cancellationToken);
            if (refreshToken == null)
            {
                throw new NotFoundException("Refresh token", command.RefreshToken);
            }
            else if (refreshToken.IsRevoked)
            {
                throw new ForbiddenException("Refresh token", command.RefreshToken);
            }

            await _unitOfWork.RefreshTokens.RevokeRefreshTokenAsync(refreshToken, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
