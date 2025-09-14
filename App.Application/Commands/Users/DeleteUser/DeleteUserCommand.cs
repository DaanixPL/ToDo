using App.Application.Interfaces.Authorizable;
using MediatR;

namespace App.Application.Commands.Users.DeleteUser
{
    public record DeleteUserCommand(int UserId) : IRequest<int>, IAuthorizableRequest
    {
        public int? ResourceOwnerId => UserId;
        public bool AllowAdminOverride => true;
    }
}
