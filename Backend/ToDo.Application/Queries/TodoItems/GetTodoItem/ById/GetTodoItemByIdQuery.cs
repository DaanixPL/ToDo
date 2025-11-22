using ToDo.Domain.Entities;
using MediatR;
using ToDo.Application.Interfaces.Authorizable;

namespace ToDo.Application.Queries.TodoItems.GetTodoItem.ById
{
    public record GetTodoItemByIdQuery (int Id) : IRequest<TodoItem>, IAuthorizableRequest
        {
        public int? ResourceOwnerId => null;
        public bool AllowAdminOverride => true;
    }
}
