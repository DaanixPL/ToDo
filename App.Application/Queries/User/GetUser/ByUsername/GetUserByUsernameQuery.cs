using App.Application.Interfaces.Authorizable;
using App.Domain.DTOs;
using MediatR;

namespace App.Application.Queries.User.GetUser.ByUsername
{
    public record GetUserByUsernameQuery(string Username) : IRequest<UserDto>, IAuthorizableRequest
    {
        public int? ResourceOwnerId => null;
        public bool AllowAdminOverride => true;
    }
}
