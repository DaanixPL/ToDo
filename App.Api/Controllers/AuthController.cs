using App.Application.Commands.TokenRefreshRequest.TokenRefresh;
using App.Application.Commands.TokenRefreshRequest.RevokeRefreshToken;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace App.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshToken(TokenRefreshRequestCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        [HttpPost("Revoked")]
        public async Task<IActionResult> Revoked(RevokeRefreshTokenCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
