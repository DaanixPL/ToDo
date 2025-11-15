using ToDo.Application.Validators.Exceptions;
using ToDo.Domain.Abstractions;
using ToDo.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ToDo.Application.Queries.TodoItems.GetTodoItem.ById
{
    public class GetTodoItemByIdQueryHandler : IRequestHandler<GetTodoItemByIdQuery, TodoItem>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetTodoItemByIdQueryHandler> _logger;

        public GetTodoItemByIdQueryHandler(IUnitOfWork unitOfWork, ILogger<GetTodoItemByIdQueryHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<TodoItem> Handle(GetTodoItemByIdQuery query, CancellationToken cancellationToken)
        {
            var todoItem = await _unitOfWork.TodoItems.GetTodoItemByIdAsync(query.Id, cancellationToken);

            if (todoItem == null)
            {
                _logger.LogWarning("Todo item with ID {TodoItemId} not found", query.Id);
                throw new NotFoundException("Todo item", query.Id);
            }
            _logger.LogInformation("Retrieved Todo item with ID {TodoItemId}", query.Id);
            return todoItem;
        }
    }
}
