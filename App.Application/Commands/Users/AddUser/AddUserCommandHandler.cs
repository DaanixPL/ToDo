using ToDo.Domain.Abstractions;
using AutoMapper;
using MediatR;
using ToDo.Domain.Entities;

namespace App.Application.Commands.Users.AddUser
{
    public class AddUserCommandHandler : IRequestHandler<AddUserCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AddUserCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<int> Handle(AddUserCommand command, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<User>(command);
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(command.Password);

            await _unitOfWork.Users.AddUserAsync(user, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return user.Id;
        }
    }
}
