using App.Application.Validators.Exceptions;
using ToDo.Domain.Abstractions;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace App.Application.Commands.Users.UpdateUser
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateUserCommandHandler> _logger;


        public UpdateUserCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<UpdateUserCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<int> Handle(UpdateUserCommand command, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.Users.GetUserByIdAsync(command.UserId, cancellationToken);

            if (user == null)
            {
                _logger.LogWarning("User with ID {UserId} not found for update", command.UserId);
                throw new NotFoundException("User", command.UserId);
            }

            _mapper.Map(command, user);

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(command.PasswordHash);

            _logger.LogInformation("Updating User with ID {UserId}", command.UserId);

            await _unitOfWork.Users.UpdateUserAsync(user, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return user.Id;
        }
    }
}
