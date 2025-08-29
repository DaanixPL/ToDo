using App.Application.Validators.Extensions;
using FluentValidation;

namespace App.Application.Queries.User.GetUser.ByEmail
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
