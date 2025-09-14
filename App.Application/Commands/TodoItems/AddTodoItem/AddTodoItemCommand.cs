using App.Application.Interfaces.Authorizable;
using MediatR;

namespace App.Application.Commands.TodoItems.AddTodoItem
{
    public record AddTodoItemCommand(string Title, string Description) : IRequest<int>, IAuthorizableRequest
    {
        public int? ResourceOwnerId => null;

        public bool AllowAdminOverride => true;
    }
}
