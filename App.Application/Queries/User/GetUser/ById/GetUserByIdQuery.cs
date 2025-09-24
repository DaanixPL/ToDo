using App.Application.Interfaces.Authorizable;
using App.Domain.DTOs;
using MediatR;

namespace App.Application.Queries.User.GetUser.ById
{
    public record GetUserByIdQuery(int UserId) : IRequest<UserDto>, IAuthorizableRequest
    {
        public int? ResourceOwnerId => UserId;
        public bool AllowAdminOverride => true;
    }
}
