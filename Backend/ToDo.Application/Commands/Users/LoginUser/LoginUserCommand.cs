using ToDo.Application.Interfaces.Authorizable;
using MediatR;
using ToDo.Application.Responses;

namespace ToDo.Application.Commands.Users.LoginUser
{
    public record LoginUserCommand(string EmailOrUsername, string Password) : IRequest<LoginUserRespone>;
}
