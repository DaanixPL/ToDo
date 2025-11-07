using App.Application.Responses;
using MediatR;

namespace App.Application.Commands.TokenRefreshRequest.TokenRefresh
{
    public record TokenRefreshRequestCommand(string AccessToken, string RefreshToken) : IRequest<RefreshTokenResponse>;
}
