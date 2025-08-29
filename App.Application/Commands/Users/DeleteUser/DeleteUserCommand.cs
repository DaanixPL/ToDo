using MediatR;

namespace App.Application.Commands.Users.DeleteUser
{
    public record DeleteUserCommand(int UserId) : IRequest<int>;
}
