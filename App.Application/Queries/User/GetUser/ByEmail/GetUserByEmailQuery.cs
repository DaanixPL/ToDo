using App.Application.Interfaces.Authorizable;
using App.Domain.DTOs;
using MediatR;

namespace App.Application.Queries.User.GetUser.ByEmail
{
    public record GetUserByEmailQuery(string Email) : IRequest<UserDto>, IAuthorizableRequest
    {
        public int? ResourceOwnerId => null;
        public bool AllowAdminOverride => true;
    }
}
