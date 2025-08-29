using App.Application.Validators.Exceptions;
using App.Domain.Abstractions;
using MediatR;

namespace App.Application.Commands.Users.DeleteUser
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteUserCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(DeleteUserCommand command, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.Users.GetUserByIdAsync(command.UserId);

            if (user == null)
            {
                throw new NotFoundException("User", command.UserId);
            }

            await _unitOfWork.Users.DeleteUserAsync(user, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return 1;
        }
    }
}
