using FluentValidation;
using ToDo.Application.Validators.Extensions;

namespace ToDo.Application.Queries.User.GetUser.ByEmail
{
    public class GetUserByEmailQueryValidator : AbstractValidator<GetUserByEmailQuery>
    {
        public GetUserByEmailQueryValidator() 
        {
            RuleFor(x => x.Email)
                .RequiredField(nameof(GetUserByEmailQuery.Email))
                .EmailFormat(nameof(GetUserByEmailQuery.Email))
                .NoWhiteSpaces(nameof(GetUserByEmailQuery.Email));
        }
    }
}
