using App.Application.Commands.Users.AddUser;
using App.Application.Commands.Users.DeleteUser;
using App.Application.Commands.Users.LoginUser;
using App.Application.Commands.Users.UpdateUser;
using App.Application.Queries.User.GetUser.ByEmail;
using App.Application.Queries.User.GetUser.ById;
using App.Application.Queries.User.GetUser.ByUsername;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace App.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost]                    // POST /api/users
        public async Task<IActionResult> AddUser(AddUserCommand command)
        {
            var userId = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetUserById), new { id = userId }, userId);
        }

        [Authorize]
        [HttpDelete("{id}")]          // DELETE /api/users/{id}
        public async Task<IActionResult> DeleteUser(int id)
        {
            await _mediator.Send(new DeleteUserCommand(id));
            return NoContent();
        }

        [Authorize]
        [HttpPut("{id}")]             // PUT /api/users/{id}
        public async Task<IActionResult> UpdateUser(int id, UpdateUserCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost("login")]           // POST /api/users/login
        public async Task<IActionResult> LoginUser(LoginUserCommand command)
        {
            try
            {
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        [Authorize]
        [HttpGet("{id}")]             // GET /api/users/{id}
        public async Task<IActionResult> GetUserById(int id)
        {
            var result = await _mediator.Send(new GetUserByIdQuery(id));
            return Ok(result);
        }

        [Authorize]
        [HttpGet("by-email")]         // GET /api/users/by-email?email=...
        public async Task<IActionResult> GetUserByEmail([FromQuery] string email)
        {
            var result = await _mediator.Send(new GetUserByEmailQuery(email));
            return Ok(result);
        }

        [Authorize]
        [HttpGet("by-username")]      // GET /api/users/by-username?username=...
        public async Task<IActionResult> GetUserByUsername([FromQuery] string username)
        {
            var result = await _mediator.Send(new GetUserByUsernameQuery(username));
            return Ok(result);
        }
    }
}
