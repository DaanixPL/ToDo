using MediatR;
using ToDo.Application.DTOs;
using ToDo.Application.Interfaces.Authorizable;

namespace ToDo.Application.Queries.User.GetUser.ById
{
    public record GetUserByIdQuery(int UserId) : IRequest<UserDto>, IAuthorizableRequest
    {
        public int? ResourceOwnerId => UserId;
        public bool AllowAdminOverride => true;
    }
}
