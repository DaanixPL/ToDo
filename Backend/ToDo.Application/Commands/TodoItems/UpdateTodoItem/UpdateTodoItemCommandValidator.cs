using ToDo.Application.Commands.TodoItems.AddTodoItem;
using ToDo.Application.Commands.Users.UpdateUser;
using FluentValidation;
using ToDo.Application.Validators.Extensions;

namespace ToDo.Application.Commands.TodoItems.UpdateTodoItem
{
    public class UpdateTodoItemCommandValidator : AbstractValidator<UpdateTodoItemCommand>
    {
        public UpdateTodoItemCommandValidator()
        {
            RuleFor(x => x.Id)
                .ValidId(nameof(UpdateTodoItemCommand.Id))
                .RequiredField(nameof(UpdateTodoItemCommand.Id));
            RuleFor(x => x.Title)
                .MaxLength(nameof(UpdateTodoItemCommand.Title), 100)
                .When(x => !string.IsNullOrWhiteSpace(x.Title));
            RuleFor(x => x.Description)
                .MaxLength(nameof(UpdateTodoItemCommand.Description), 500)
                .When(x => !string.IsNullOrWhiteSpace(x.Description));
        }
    }
}
