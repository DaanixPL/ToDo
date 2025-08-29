using App.Application.Validators.Extensions;
using App.Domain.Abstractions;
using FluentValidation;

namespace App.Application.Commands.Users.AddUser
{
    public class AddUserCommandValidator : AbstractValidator<AddUserCommand>
    {
        public AddUserCommandValidator(IUnitOfWork unitOfWork) 
        {
            RuleFor(x => x.Username)
                .RequiredField(nameof(AddUserCommand.Username))
                .MaxLength(nameof(AddUserCommand.Username), 50)
                .MinLength(nameof(AddUserCommand.Username), 3)
                .NoWhiteSpaces(nameof(AddUserCommand.Username))
                .NoAlreadyExistsAsync(
                async (username, cancellation) =>
                    await unitOfWork.Users.GetUserByUsernameAsync(username, cancellation) == null,
                nameof(AddUserCommand.Username));

            RuleFor(x => x.Email)
                .RequiredField(nameof(AddUserCommand.Email))
                .EmailFormat(nameof(AddUserCommand.Email))
                .NoWhiteSpaces(nameof(AddUserCommand.Email))
                .NoAlreadyExistsAsync(
                 async (email, cancellation) =>
                 {
                     var user = await unitOfWork.Users.GetUserByEmailAsync(email, cancellation);
                     return user == null;
                 },
                nameof(AddUserCommand.Email));

            RuleFor(x => x.Password)
                .RequiredField(nameof(AddUserCommand.Password))
                .MaxLength(nameof(AddUserCommand.Password), 128)
                .MinLength(nameof(AddUserCommand.Password), 8);
        }
    }
}
