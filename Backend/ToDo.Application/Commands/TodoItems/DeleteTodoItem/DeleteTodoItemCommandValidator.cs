using FluentValidation;
using ToDo.Application.Validators.Extensions;

namespace ToDo.Application.Commands.TodoItems.DeleteTodoItem
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
