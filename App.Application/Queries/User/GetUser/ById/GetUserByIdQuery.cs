using App.Domain.DTOs;
using MediatR;

namespace App.Application.Queries.User.GetUser.ById
{
    public record GetUserByIdQuery(int UserId) : IRequest<UserDto>;
}
