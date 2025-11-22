using MediatR;
using ToDo.Application.Interfaces.Authorizable;

namespace ToDo.Application.Commands.Users.UpdateUser
{
    public class UpdateUserCommand : IRequest<int>, IAuthorizableRequest
    {
        public int UserId { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? PasswordHash { get; set; }

        public int? ResourceOwnerId => null;

        public bool AllowAdminOverride => true;
    }
}
