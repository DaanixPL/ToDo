using MediatR;
using ToDo.Application.DTOs;
using ToDo.Application.Interfaces.Authorizable;

namespace ToDo.Application.Queries.User.GetUser.ByUsername
{
    public record GetUserByUsernameQuery(string Username) : IRequest<UserDto>, IAuthorizableRequest
    {
        public int? ResourceOwnerId => null;
        public bool AllowAdminOverride => true;
    }
}
