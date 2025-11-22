using ToDo.Application.Validators.Exceptions;
using ToDo.Domain.Abstractions;
using ToDo.Domain.Entities;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ToDo.Application.Commands.TodoItems.UpdateTodoItem
{
    public class UpdateTodoItemCommandHandler : IRequestHandler<UpdateTodoItemCommand, TodoItem>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateTodoItemCommandHandler> _logger;

        public UpdateTodoItemCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<UpdateTodoItemCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<TodoItem> Handle(UpdateTodoItemCommand command, CancellationToken cancellationToken)
        {
            var todoItem = await _unitOfWork.TodoItems.GetTodoItemByIdAsync(command.Id, cancellationToken);

            if (todoItem == null)
            {
                _logger.LogWarning("Todo item with ID {TodoItemId} not found for update", command.Id);
                throw new NotFoundException("Todo Item", command.Id);
            }

            _mapper.Map(command, todoItem);

            if(command.IsCompleted.HasValue && command.IsCompleted.Value == false)
            {
                todoItem.CompletedAt = null;
            }

            _logger.LogInformation("Updating Todo item with ID {TodoItemId}", todoItem.Id);
            await _unitOfWork.TodoItems.UpdateTodoItemAsync(todoItem, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return todoItem;
        }
    }
}
