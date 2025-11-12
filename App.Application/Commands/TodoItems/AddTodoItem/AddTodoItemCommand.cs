using App.Application.Interfaces.Authorizable;
using MediatR;
using ToDo.Domain.Entities;

namespace App.Application.Commands.TodoItems.AddTodoItem
{
    public record AddTodoItemCommand(string Title, string Description) : IRequest<TodoItem>, IAuthorizableRequest
    {
        public int? ResourceOwnerId => null;

        public bool AllowAdminOverride => true;
    }
}
