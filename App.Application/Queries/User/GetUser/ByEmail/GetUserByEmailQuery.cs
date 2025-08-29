using App.Domain.DTOs;
using MediatR;

namespace App.Application.Queries.User.GetUser.ByEmail
{
    public record GetUserByEmailQuery(string Email) : IRequest<UserDto>;
}
