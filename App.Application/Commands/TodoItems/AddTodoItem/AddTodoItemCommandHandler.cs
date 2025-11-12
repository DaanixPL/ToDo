using ToDo.Domain.Abstractions;
using ToDo.Domain.Entities;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace App.Application.Commands.TodoItems.AddTodoItem
{
    public class AddTodoItemCommandHandler : IRequestHandler<AddTodoItemCommand, TodoItem>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<AddTodoItemCommandHandler> _logger;

        public AddTodoItemCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor, ILogger<AddTodoItemCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }

        public async Task<TodoItem> Handle(AddTodoItemCommand command, CancellationToken cancellationToken)
        {
            TodoItem todoItem = _mapper.Map<TodoItem>(command);

            var user = _httpContextAccessor.HttpContext?.User;
            var userIdClaim = user?.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            
            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out var userId))
            {
                throw new UnauthorizedAccessException("User ID not found in token");
            }
               
            todoItem.UserId = userId;

            await _unitOfWork.TodoItems.AddTodoItemAsync(todoItem);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Todo item with ID {TodoItemId} created by User {UserId}", todoItem.Id, todoItem.UserId);
            return todoItem;
        }
    }
}