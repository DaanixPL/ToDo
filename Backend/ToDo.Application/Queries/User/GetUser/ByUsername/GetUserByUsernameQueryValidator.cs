using FluentValidation;
using ToDo.Application.Validators.Extensions;

namespace ToDo.Application.Queries.User.GetUser.ByUsername
{
    public class GetUserByUsernameQueryValidator : AbstractValidator<GetUserByUsernameQuery>
    {
        public GetUserByUsernameQueryValidator() 
        {
            RuleFor(x => x.Username)
               .RequiredField(nameof(GetUserByUsernameQuery.Username))
               .MaxLength(nameof(GetUserByUsernameQuery.Username), 50)
               .MinLength(nameof(GetUserByUsernameQuery.Username), 3)
               .NoWhiteSpaces(nameof(GetUserByUsernameQuery.Username));
        }
    }
}
