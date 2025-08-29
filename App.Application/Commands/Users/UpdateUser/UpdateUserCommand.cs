using MediatR;

namespace App.Application.Commands.Users.UpdateUser
{
    public class UpdateUserCommand : IRequest<int>
    {
        public int UserId { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? PasswordHash { get; set; }
    }
}
