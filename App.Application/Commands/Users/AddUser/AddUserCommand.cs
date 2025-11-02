using App.Application.Interfaces.Authorizable;
using MediatR;

namespace App.Application.Commands.Users.AddUser
{
    public record AddUserCommand(string Username, string Email, string Password) : IRequest<int>;
}
