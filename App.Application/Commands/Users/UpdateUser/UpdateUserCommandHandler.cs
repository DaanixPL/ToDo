using App.Application.Validators.Exceptions;
using ToDo.Domain.Abstractions;
using AutoMapper;
using MediatR;

namespace App.Application.Commands.Users.UpdateUser
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;


        public UpdateUserCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<int> Handle(UpdateUserCommand command, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.Users.GetUserByIdAsync(command.UserId, cancellationToken);

            if (user == null)
            {
                throw new NotFoundException("User", command.UserId);
            }

            _mapper.Map(command, user);

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(command.PasswordHash);

            await _unitOfWork.Users.UpdateUserAsync(user, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return user.Id;
        }
    }
}
