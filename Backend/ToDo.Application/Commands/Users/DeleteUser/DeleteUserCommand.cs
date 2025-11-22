using MediatR;
using ToDo.Application.Interfaces.Authorizable;

namespace ToDo.Application.Commands.Users.DeleteUser
{
    public record DeleteUserCommand(int UserId) : IRequest<int>, IAuthorizableRequest
    {
        public int? ResourceOwnerId => UserId;
        public bool AllowAdminOverride => true;
    }
}
