using App.Application.Authentication;
using App.Application.Responses;
using App.Domain.Abstractions;
using App.Domain.Entities;
using MediatR;

namespace App.Application.Commands.Users.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, LoginUserRespone>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenGeneratorRepository _tokenGenerator;

        public LoginUserCommandHandler(IUnitOfWork unitOfWork, ITokenGeneratorRepository tokenGenerator)
        {
            _tokenGenerator = tokenGenerator;
            _unitOfWork = unitOfWork;
        }

        public async Task<LoginUserRespone> Handle(LoginUserCommand command, CancellationToken cancellationToken)
        {
            User? user;
            if(command.EmailOrUsername.Contains("@"))
            {
                user = await _unitOfWork.Users.GetUserByEmailAsync(command.EmailOrUsername, cancellationToken);
            }
            else
            {
                user = await _unitOfWork.Users.GetUserByUsernameAsync(command.EmailOrUsername, cancellationToken);
            }

            if (user == null)
            {
                throw new UnauthorizedAccessException("Invalid credentials.");
            }

            var isValidPassword = BCrypt.Net.BCrypt.Verify(command.Password, user.PasswordHash);

            if (!isValidPassword)
            {
                throw new UnauthorizedAccessException("Invalid credentials.");
            }

            user.LastLoginDate = DateTime.UtcNow;
            await _unitOfWork.Users.UpdateUserAsync(user, cancellationToken);

            var accessToken = _tokenGenerator.GenerateAccessToken(user);

            var refreshTokenId = Guid.NewGuid();
            var refreshToken = _tokenGenerator.GenerateRefreshToken(user, refreshTokenId);

            var refreshTokenEntity = new RefreshToken
            {
                Token = refreshToken,
                ExpiresAt = DateTime.UtcNow.AddHours(720),
                UserId = user.Id,
                User = user
            };

            await _unitOfWork.RefreshTokens.AddRefreshTokenAsync(refreshTokenEntity, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new LoginUserRespone(user.Id, user.Username, accessToken, refreshToken);
        }
    }
}
