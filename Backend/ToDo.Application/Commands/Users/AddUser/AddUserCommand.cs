using ToDo.Application.Interfaces.Authorizable;
using MediatR;

namespace ToDo.Application.Commands.Users.AddUser
{
    public record AddUserCommand(string Username, string Email, string Password) : IRequest<int>;
}
