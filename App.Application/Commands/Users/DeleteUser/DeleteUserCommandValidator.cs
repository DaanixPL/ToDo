using App.Application.Commands.Users.AddUser;
using App.Application.Validators.Extensions;
using FluentValidation;

namespace App.Application.Commands.Users.DeleteUser
{
    public class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
    {
        public DeleteUserCommandValidator() 
        {
            RuleFor(x => x.UserId)
                .RequiredField(nameof(DeleteUserCommand.UserId));
        }
    }
}
