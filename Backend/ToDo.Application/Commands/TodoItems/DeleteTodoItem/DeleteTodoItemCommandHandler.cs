using ToDo.Application.Validators.Exceptions;
using ToDo.Domain.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace ToDo.Application.Commands.TodoItems.DeleteTodoItem
{
    public class DeleteTodoItemCommandHandler : IRequestHandler<DeleteTodoItemCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<DeleteTodoItemCommandHandler> _logger;

        public DeleteTodoItemCommandHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, ILogger<DeleteTodoItemCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }

        public async Task<int> Handle(DeleteTodoItemCommand command, CancellationToken cancellationToken)
        {
            var todoItem = await _unitOfWork.TodoItems.GetTodoItemByIdAsync(command.TodoItemId);

            if (todoItem == null)
            {
                _logger.LogWarning("Todo item with ID {TodoItemId} not found for deletion", command.TodoItemId);
                throw new NotFoundException("Todo Item", command.TodoItemId);
            }

            _logger.LogInformation("Deleting Todo item with ID {TodoItemId}", command.TodoItemId);

            await _unitOfWork.TodoItems.DeleteTodoItemAsync(todoItem, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return todoItem.Id;
        }
    }
}
