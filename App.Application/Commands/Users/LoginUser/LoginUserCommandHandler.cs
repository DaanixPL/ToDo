using App.Application.Interfaces.Authentication;
using App.Application.Responses;
using ToDo.Domain.Abstractions;
using ToDo.Domain.Entities;
using MediatR;
using App.Application.Validators.Exceptions;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;

namespace App.Application.Commands.Users.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, LoginUserRespone>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenGeneratorRepository _tokenGenerator;
        private readonly ILogger<LoginUserCommandHandler> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor = new HttpContextAccessor();

        public LoginUserCommandHandler(IUnitOfWork unitOfWork, ITokenGeneratorRepository tokenGenerator, ILogger<LoginUserCommandHandler> logger, IHttpContextAccessor httpContextAccessor)
        {
            _tokenGenerator = tokenGenerator;
            _unitOfWork = unitOfWork;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
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
                _logger.LogWarning("Login attempt failed. User with identifier {EmailOrUsername} not found.", command.EmailOrUsername);
                throw new NotFoundException("User", command.EmailOrUsername);
            }

            var isValidPassword = BCrypt.Net.BCrypt.Verify(command.Password, user.PasswordHash);

            if (!isValidPassword)
            {
                _logger.LogWarning("Login attempt failed for User ID {UserId} from IP: {RemoteIp}. Invalid password provided.", user.Id, _httpContextAccessor.HttpContext?.Connection.RemoteIpAddress);
                throw new UnauthorizedAccessException("Invalid Password.");
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

            _logger.LogInformation("User ID {UserId} logged in successfully from IP: {RemoteIp}.", user.Id, _httpContextAccessor.HttpContext?.Connection.RemoteIpAddress);

            return new LoginUserRespone(user.Id, user.Username, accessToken, refreshToken);
        }
    }
}
