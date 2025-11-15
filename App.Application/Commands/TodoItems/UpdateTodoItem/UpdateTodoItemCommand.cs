using MediatR;
using ToDo.Application.Interfaces.Authorizable;
using ToDo.Domain.Entities;

namespace ToDo.Application.Commands.TodoItems.UpdateTodoItem
{
    public class UpdateTodoItemCommand : IRequest<TodoItem>, IAuthorizableRequest
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTimeOffset? CompletedAt { get; set; }
        public bool? IsCompleted { get; set; }

        public int? ResourceOwnerId => null;

        public bool AllowAdminOverride => true;
    }
}
