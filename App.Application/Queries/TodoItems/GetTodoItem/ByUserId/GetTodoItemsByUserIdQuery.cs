using ToDo.Domain.Entities;
using MediatR;
using ToDo.Application.Interfaces.Authorizable;

namespace ToDo.Application.Queries.TodoItems.GetTodoItem.ByUserId
{
    public record GetTodoItemsByUserIdQuery(int UserId) : IRequest<IEnumerable<TodoItem>>, IAuthorizableRequest
    {
        public int? ResourceOwnerId => UserId;
        public bool AllowAdminOverride => true;
    }
}
