using App.Application.Interfaces.Authorizable;
using App.Application.Responses;
using MediatR;

namespace App.Application.Commands.Users.LoginUser
{
    public record LoginUserCommand(string EmailOrUsername, string Password) : IRequest<LoginUserRespone>;
}
