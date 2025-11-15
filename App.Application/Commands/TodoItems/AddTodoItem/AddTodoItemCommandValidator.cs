using FluentValidation;
using ToDo.Application.Validators.Extensions;

namespace ToDo.Application.Commands.TodoItems.AddTodoItem
{
    public class AddTodoItemCommandValidator : AbstractValidator<AddTodoItemCommand>
    {
        public AddTodoItemCommandValidator()
        {
            RuleFor(x => x.Title)
                .RequiredField(nameof(AddTodoItemCommand.Title))
                .MaxLength(nameof(AddTodoItemCommand.Title), 100);
            RuleFor(x => x.Description)
                .MaxLength(nameof(AddTodoItemCommand.Description), 500);
        }
    }
}
