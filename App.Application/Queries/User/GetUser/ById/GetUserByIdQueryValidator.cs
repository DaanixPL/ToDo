using App.Application.Validators.Extensions;
using FluentValidation;

namespace App.Application.Queries.User.GetUser.ById
{
    public class GetUserByIdQueryValidator : AbstractValidator<GetUserByIdQuery>
    {
        public GetUserByIdQueryValidator() 
        {
            RuleFor(x => x.UserId)
                .RequiredField(nameof(GetUserByIdQuery.UserId));
        }
    }
}
