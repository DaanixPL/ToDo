using App.Application.Interfaces.Authorizable;
using App.Domain.Entities;
using MediatR;

namespace App.Application.Queries.TodoItems.GetTodoItem.ByUserId
{
    public record GetTodoItemsByUserIdQuery(int UserId) : IRequest<IEnumerable<TodoItem>>, IAuthorizableRequest
    {
        public int? ResourceOwnerId => UserId;
        public bool AllowAdminOverride => true;
    }
}
