using App.Application.Validators.Extensions;
using FluentValidation;

namespace App.Application.Queries.TodoItems.GetTodoItem.ById
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
