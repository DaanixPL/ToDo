using App.Application.Interfaces.Authorizable;
using MediatR;

namespace App.Application.Commands.TodoItems.DeleteTodoItem
{
    public record DeleteTodoItemCommand(int TodoItemId) : IRequest<int>, IAuthorizableRequest
    {
        public int? ResourceOwnerId => null;
        public bool AllowAdminOverride => true;
}
}
