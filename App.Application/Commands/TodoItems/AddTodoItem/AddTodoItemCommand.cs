using MediatR;
using ToDo.Application.Interfaces.Authorizable;
using ToDo.Domain.Entities;

namespace ToDo.Application.Commands.TodoItems.AddTodoItem
{
    public record AddTodoItemCommand(string Title, string Description) : IRequest<TodoItem>, IAuthorizableRequest
    {
        public int? ResourceOwnerId => null;

        public bool AllowAdminOverride => true;
    }
}
