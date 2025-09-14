using App.Application.Validators.Extensions;
using FluentValidation;

namespace App.Application.Commands.TodoItems.DeleteTodoItem
{
    public class DeleteTodoItemCommandValidator : AbstractValidator<DeleteTodoItemCommand>
    {
        public DeleteTodoItemCommandValidator()
        {
            RuleFor(x => x.TodoItemId)
                .ValidId(nameof(DeleteTodoItemCommand.TodoItemId));
        }
    }
}
