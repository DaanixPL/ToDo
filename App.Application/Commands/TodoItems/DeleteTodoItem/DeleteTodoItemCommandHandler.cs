using App.Application.Validators.Exceptions;
using ToDo.Domain.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace App.Application.Commands.TodoItems.DeleteTodoItem
{
    public class DeleteTodoItemCommandHandler : IRequestHandler<DeleteTodoItemCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DeleteTodoItemCommandHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<int> Handle(DeleteTodoItemCommand command, CancellationToken cancellationToken)
        {
            var todoItem = await _unitOfWork.TodoItems.GetTodoItemByIdAsync(command.TodoItemId);

            if (todoItem == null)
            {
                throw new NotFoundException("Todo Item", command.TodoItemId);
            }

            await _unitOfWork.TodoItems.DeleteTodoItemAsync(todoItem, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return 1;
        }
    }
}
