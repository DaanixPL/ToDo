using MediatR;
using ToDo.Application.DTOs;
using ToDo.Application.Interfaces.Authorizable;

namespace ToDo.Application.Queries.User.GetUser.ByEmail
{
    public record GetUserByEmailQuery(string Email) : IRequest<UserDto>, IAuthorizableRequest
    {
        public int? ResourceOwnerId => null;
        public bool AllowAdminOverride => true;
    }
}
