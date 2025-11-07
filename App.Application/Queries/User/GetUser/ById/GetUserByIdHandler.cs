using App.Application.Validators.Exceptions;
using ToDo.Domain.Abstractions;
using App.Domain.DTOs;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace App.Application.Queries.User.GetUser.ById
{
    public class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, UserDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<GetUserByIdHandler> _logger;
        public GetUserByIdHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<GetUserByIdHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<UserDto> Handle(GetUserByIdQuery query, CancellationToken cancellation)
        {
            var user = await _unitOfWork.Users.GetUserByIdAsync(query.UserId);

            if (user == null)
            {
                _logger.LogWarning("User with ID {UserId} not found", query.UserId);
                throw new NotFoundException("User", query.UserId);
            }

            _logger.LogInformation("Retrieved User with ID {UserId}", query.UserId);
            return _mapper.Map<UserDto>(user);
        }
    }
}
