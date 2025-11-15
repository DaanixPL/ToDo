using ToDo.Application.Validators.Exceptions;
using ToDo.Domain.Abstractions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ToDo.Application.Commands.Users.DeleteUser
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<DeleteUserCommandHandler> _logger;

        public DeleteUserCommandHandler(IUnitOfWork unitOfWork, ILogger<DeleteUserCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<int> Handle(DeleteUserCommand command, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.Users.GetUserByIdAsync(command.UserId);

            if (user == null)
            {
                _logger.LogWarning("User with ID {UserId} not found for deletion", command.UserId);
                throw new NotFoundException("User", command.UserId);
            }

            _logger.LogInformation("Deleting User with ID {UserId}", command.UserId);

            await _unitOfWork.Users.DeleteUserAsync(user, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return 1;
        }
    }
}
