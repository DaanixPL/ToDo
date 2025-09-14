using App.Application.Validators.Extensions;
using FluentValidation;

namespace App.Application.Commands.TodoItems.AddTodoItem
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
