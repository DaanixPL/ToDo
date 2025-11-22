using MediatR;
using ToDo.Application.Responses;

namespace ToDo.Application.Commands.TokenRefreshRequest.TokenRefresh
{
    public record TokenRefreshRequestCommand(string AccessToken, string RefreshToken) : IRequest<RefreshTokenResponse>;
}
