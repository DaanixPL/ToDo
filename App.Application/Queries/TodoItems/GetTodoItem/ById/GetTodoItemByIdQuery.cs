using App.Application.Interfaces.Authorizable;
using ToDo.Domain.Entities;
using MediatR;

namespace App.Application.Queries.TodoItems.GetTodoItem.ById
{
    public record GetTodoItemByIdQuery (int Id) : IRequest<TodoItem>, IAuthorizableRequest
        {
        public int? ResourceOwnerId => null;
        public bool AllowAdminOverride => true;
    }
}
