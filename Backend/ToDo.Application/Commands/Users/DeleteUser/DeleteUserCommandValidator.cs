using ToDo.Application.Commands.Users.AddUser;
using FluentValidation;
using ToDo.Application.Validators.Extensions;

namespace ToDo.Application.Commands.Users.DeleteUser
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
