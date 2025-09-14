using App.Application.Commands.TodoItems.AddTodoItem;
using App.Application.Commands.TodoItems.DeleteTodoItem;
using App.Application.Commands.TodoItems.UpdateTodoItem;
using App.Application.Queries.TodoItems.GetTodoItem.ById;
using App.Application.Queries.TodoItems.GetTodoItem.ByUserId;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace App.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoItemsController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost]                    // POST /api/todoitems
        public async Task<IActionResult> AddTodoItem(AddTodoItemCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }

        [Authorize]
        [HttpDelete("{id}")]          // DELETE /api/todoitems/{id}
        public async Task<IActionResult> DeleteTodoItem(int id)
        {
            await _mediator.Send(new DeleteTodoItemCommand(id));
            return NoContent();
        }

        [Authorize]
        [HttpPut("{id}")]             // PUT /api/todoitems/{id}
        public async Task<IActionResult> UpdateTodoItem(int id, UpdateTodoItemCommand command)
        {
            await _mediator.Send(new UpdateTodoItemCommand
            {
                Id = id,
                Title = command.Title,
                Description = command.Description,
                CompletedAt = command.CompletedAt,
                IsCompleted = command.IsCompleted
            });
            return NoContent();
        }

        [Authorize]
        [HttpGet("{id}")]             // GET /api/todoitems/{id}
        public async Task<IActionResult> GetTodoItemById(int id)
        {
            var result = await _mediator.Send(new GetTodoItemByIdQuery(id));
            return Ok(result);
        }
        [Authorize]
        [HttpGet("user/{userId}")]    // GET /api/todoitems/user/{userId}
        public async Task<IActionResult> GetTodoItemsByUserId(int userId)
        {
            var result = await _mediator.Send(new GetTodoItemsByUserIdQuery(userId));
            return Ok(result);
        }
        [Authorize]
        [HttpGet("me")]    // GET /api/todoitems/me
        public async Task<IActionResult> GetTodoItemsByUserId()
        {
            var userId = int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value ?? "0");
            var result = await _mediator.Send(new GetTodoItemsByUserIdQuery(userId));
            return Ok(result);
        }
    }
}
