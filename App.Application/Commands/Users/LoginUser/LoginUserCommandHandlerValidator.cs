using App.Application.Commands.Users.AddUser;
using App.Application.Validators.Extensions;
using FluentValidation;

namespace App.Application.Commands.Users.LoginUser
{
    public class LoginUserCommandHandlerValidator : AbstractValidator<LoginUserCommand>
    {
        public LoginUserCommandHandlerValidator() 
        {
            RuleFor(x => x.Password)
                .RequiredField(nameof(LoginUserCommand.Password));

            When(x => x.EmailOrUsername.Contains("@"), () => 
            {
                RuleFor(x => x.EmailOrUsername)
                    .RequiredField(nameof(LoginUserCommand.EmailOrUsername))
                    .EmailFormat(nameof(LoginUserCommand.EmailOrUsername))
                    .NoWhiteSpaces(nameof(LoginUserCommand.EmailOrUsername));
            });


            When(x => !x.EmailOrUsername.Contains("@"), () =>
            {
                RuleFor(x => x.EmailOrUsername)
                    .RequiredField(nameof(AddUserCommand.Username))
                    .NoWhiteSpaces(nameof(AddUserCommand.Username));
            });
                    
        }
    }
}
