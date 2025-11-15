using MediatR;
using Microsoft.AspNetCore.Http;

namespace ToDo.Application.Commands.TokenRefreshRequest.RevokeRefreshToken
{
    public record RevokeRefreshTokenCommand(string RefreshToken) : IRequest<Unit>;
}
