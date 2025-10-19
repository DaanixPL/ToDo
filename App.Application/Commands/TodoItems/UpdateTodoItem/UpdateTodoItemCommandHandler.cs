using App.Application.Validators.Exceptions;
using ToDo.Domain.Abstractions;
using ToDo.Domain.Entities;
using AutoMapper;
using MediatR;

namespace App.Application.Commands.TodoItems.UpdateTodoItem
{
    public class UpdateTodoItemCommandHandler : IRequestHandler<UpdateTodoItemCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateTodoItemCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<int> Handle(UpdateTodoItemCommand command, CancellationToken cancellationToken)
        {
            var todoItem = await _unitOfWork.TodoItems.GetTodoItemByIdAsync(command.Id, cancellationToken);

            if (todoItem == null)
            {
                throw new NotFoundException("Todo Item", command.Id);
            }

            _mapper.Map(command, todoItem);

            await _unitOfWork.TodoItems.UpdateTodoItemAsync(todoItem, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return todoItem.Id;
        }
    }
}
