using FluentValidation;
using ToDo.Application.Validators.Extensions;

namespace ToDo.Application.Queries.TodoItems.GetTodoItem.ByUserId
{
    public class GetTodoItemsByUserIdQueryValidator : AbstractValidator<GetTodoItemsByUserIdQuery>
    {
        public GetTodoItemsByUserIdQueryValidator()
        {
            RuleFor(x => x.UserId)
                .RequiredField(nameof(GetTodoItemsByUserIdQuery.UserId))
                .ValidId(nameof(GetTodoItemsByUserIdQuery.UserId));
        }
    }
}
