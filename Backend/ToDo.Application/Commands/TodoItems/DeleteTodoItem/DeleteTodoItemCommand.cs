using MediatR;
using ToDo.Application.Interfaces.Authorizable;

namespace ToDo.Application.Commands.TodoItems.DeleteTodoItem
{
    public record DeleteTodoItemCommand(int TodoItemId) : IRequest<int>, IAuthorizableRequest
    {
        public int? ResourceOwnerId => null;
        public bool AllowAdminOverride => true;
}
}
