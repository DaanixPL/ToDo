using FluentValidation;
using ToDo.Application.Commands.Users.AddUser;
using ToDo.Application.Validators.Extensions;

namespace ToDo.Application.Commands.Users.LoginUser
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
