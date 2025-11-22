using FluentValidation;
using ToDo.Application.Validators.Extensions;

namespace ToDo.Application.Queries.User.GetUser.ById
{
    public class GetUserByIdQueryValidator : AbstractValidator<GetUserByIdQuery>
    {
        public GetUserByIdQueryValidator() 
        {
            RuleFor(x => x.UserId)
                .RequiredField(nameof(GetUserByIdQuery.UserId))
                .ValidId(nameof(GetUserByIdQuery.UserId));
        }
    }
}
