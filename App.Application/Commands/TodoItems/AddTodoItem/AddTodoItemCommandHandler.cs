using App.Application.DTOs;
using App.Domain.Abstractions;
using App.Domain.Entities;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace App.Application.Commands.TodoItems.AddTodoItem
{
    public class AddTodoItemCommandHandler : IRequestHandler<AddTodoItemCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AddTodoItemCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<int> Handle(AddTodoItemCommand command, CancellationToken cancellationToken)
        {
            TodoItem todoItem = _mapper.Map<TodoItem>(command);

            var user = _httpContextAccessor.HttpContext?.User;

            if (user?.Identity?.IsAuthenticated != true)
                throw new UnauthorizedAccessException();

            todoItem.UserId = int.Parse(user.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value ?? "0");

            await _unitOfWork.TodoItems.AddTodoItemAsync(todoItem);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return todoItem.Id;
        }
    }
}
