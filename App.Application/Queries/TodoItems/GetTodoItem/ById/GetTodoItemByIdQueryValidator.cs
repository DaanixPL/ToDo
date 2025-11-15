using FluentValidation;
using ToDo.Application.Validators.Extensions;

namespace ToDo.Application.Queries.TodoItems.GetTodoItem.ById
{
    public class GetTodoItemByIdQueryValidator : AbstractValidator<GetTodoItemByIdQuery>
    {
        public GetTodoItemByIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .RequiredField(nameof(GetTodoItemByIdQuery.Id))
                .ValidId(nameof(GetTodoItemByIdQuery.Id));
        }
    }
}
