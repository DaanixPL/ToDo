using App.Application.Interfaces.Authorizable;
using App.Domain.Entities;
using MediatR;

namespace App.Application.Queries.TodoItems.GetTodoItem.ById
{
    public record GetTodoItemByIdQuery (int Id) : IRequest<TodoItem>, IAuthorizableRequest
        {
        public int? ResourceOwnerId => null;
        public bool AllowAdminOverride => true;
    }
}
