using App.Domain.DTOs;
using MediatR;

namespace App.Application.Queries.User.GetUser.ByUsername
{
    public record GetUserByUsernameQuery(string Username) : IRequest<UserDto>;
}
