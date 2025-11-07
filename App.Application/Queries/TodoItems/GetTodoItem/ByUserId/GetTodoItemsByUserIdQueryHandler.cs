using App.Application.Validators.Exceptions;
using ToDo.Domain.Abstractions;
using ToDo.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace App.Application.Queries.TodoItems.GetTodoItem.ByUserId
{
    public class GetTodoItemsByUserIdQueryHandler : IRequestHandler<GetTodoItemsByUserIdQuery, IEnumerable<TodoItem>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetTodoItemsByUserIdQueryHandler> _logger;

        public GetTodoItemsByUserIdQueryHandler(IUnitOfWork unitOfWork, ILogger<GetTodoItemsByUserIdQueryHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<IEnumerable<TodoItem>> Handle(GetTodoItemsByUserIdQuery query, CancellationToken cancellationToken)
        {
            IEnumerable<TodoItem?> todoItems = await _unitOfWork.TodoItems.GetTodoItemsByUserIdAsync(query.UserId, cancellationToken);

            if (todoItems is null || !todoItems.Any())
            {
                _logger.LogWarning("No Todo items found for User ID {UserId}", query.UserId);
                throw new NotFoundException("Todo items", query.UserId);
            }

            _logger.LogInformation("Retrieved {Count} Todo items for User ID {UserId}", todoItems.Count(), query.UserId);
            return todoItems.Where(x => x != null)!;
        }
    }
}
