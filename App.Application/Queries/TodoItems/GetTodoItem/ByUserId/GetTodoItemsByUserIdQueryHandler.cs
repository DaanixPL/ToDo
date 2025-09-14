using App.Application.Validators.Exceptions;
using App.Domain.Abstractions;
using App.Domain.Entities;
using MediatR;

namespace App.Application.Queries.TodoItems.GetTodoItem.ByUserId
{
    public class GetTodoItemsByUserIdQueryHandler : IRequestHandler<GetTodoItemsByUserIdQuery, IEnumerable<TodoItem>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetTodoItemsByUserIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<TodoItem>> Handle(GetTodoItemsByUserIdQuery query, CancellationToken cancellationToken)
        {
            IEnumerable<TodoItem?> todoItems = await _unitOfWork.TodoItems.GetTodoItemsByUserIdAsync(query.UserId, cancellationToken);

            if (todoItems is null || !todoItems.Any())
            {
                throw new NotFoundException("Todo items", query.UserId);
            }

            return todoItems.Where(x => x != null)!;
        }
    }
}
