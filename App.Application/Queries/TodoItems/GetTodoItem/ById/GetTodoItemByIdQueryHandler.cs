using App.Application.Validators.Exceptions;
using App.Domain.Abstractions;
using App.Domain.Entities;
using MediatR;

namespace App.Application.Queries.TodoItems.GetTodoItem.ById
{
    public class GetTodoItemByIdQueryHandler : IRequestHandler<GetTodoItemByIdQuery, TodoItem>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetTodoItemByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<TodoItem> Handle(GetTodoItemByIdQuery query, CancellationToken cancellationToken)
        {
            var todoItem = await _unitOfWork.TodoItems.GetTodoItemByIdAsync(query.Id, cancellationToken);

            if (todoItem == null)
            {
                throw new NotFoundException("Todo item", query.Id);
            }

            return todoItem;
        }
    }
}
