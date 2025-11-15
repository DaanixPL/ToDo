using ToDo.Application.Commands.Users.AddUser;
using ToDo.Application.Commands.Users.DeleteUser;
using FluentValidation;
using ToDo.Application.Validators.Extensions;

namespace ToDo.Application.Commands.Users.UpdateUser
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(x => x.UserId)
                .RequiredField(nameof(UpdateUserCommand.UserId));

            RuleFor(x => x.Username)
                .MaxLength(nameof(UpdateUserCommand.Username), 50)
                .MinLength(nameof(UpdateUserCommand.Username), 3)
                .NoWhiteSpaces(nameof(UpdateUserCommand.Username))
                .When(x => !string.IsNullOrWhiteSpace(x.Username));

            RuleFor(x => x.Email)
                .EmailFormat(nameof(UpdateUserCommand.Email))
                .NoWhiteSpaces(nameof(UpdateUserCommand.Email))
                .When(x => !string.IsNullOrWhiteSpace(x.Email));

            RuleFor(x => x.PasswordHash)
                .MaxLength(nameof(UpdateUserCommand.PasswordHash), 128)
                .MinLength(nameof(UpdateUserCommand.PasswordHash), 8)
                .NoWhiteSpaces(nameof(UpdateUserCommand.PasswordHash))
                .When(x => !string.IsNullOrWhiteSpace(x.PasswordHash));
        }
    }

}
