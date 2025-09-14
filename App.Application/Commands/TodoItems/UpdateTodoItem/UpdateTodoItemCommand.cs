using App.Application.Interfaces.Authorizable;
using MediatR;

namespace App.Application.Commands.TodoItems.UpdateTodoItem
{
    public class UpdateTodoItemCommand : IRequest<int>, IAuthorizableRequest
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? CompletedAt { get; set; }
        public bool? IsCompleted { get; set; }

        public int? ResourceOwnerId => null;

        public bool AllowAdminOverride => true;
    }
}
