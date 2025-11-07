using ToDo.Domain.Abstractions;
using AutoMapper;
using MediatR;
using ToDo.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace App.Application.Commands.Users.AddUser
{
    public class AddUserCommandHandler : IRequestHandler<AddUserCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<AddUserCommandHandler> _logger;

        public AddUserCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<AddUserCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<int> Handle(AddUserCommand command, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<User>(command);
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(command.Password);

            _logger.LogInformation("Creating new user with Username {Username} and Email {Email}", user.Username, user.Email);

            await _unitOfWork.Users.AddUserAsync(user, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return user.Id;
        }
    }
}
