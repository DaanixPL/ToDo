using App.Application.Validators.Exceptions;
using ToDo.Domain.Abstractions;
using App.Domain.DTOs;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace App.Application.Queries.User.GetUser.ByEmail
{
    public class GetUserByEmailHandler : IRequestHandler<GetUserByEmailQuery, UserDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<GetUserByEmailHandler> _logger;

        public GetUserByEmailHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<GetUserByEmailHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<UserDto> Handle(GetUserByEmailQuery query, CancellationToken cancellation)
        {
            var user = await _unitOfWork.Users.GetUserByEmailAsync(query.Email, cancellation);

            if (user == null)
            {
                _logger.LogWarning("User with Email {Email} not found", query.Email);
                throw new NotFoundException("User", query.Email);
            }

            _logger.LogInformation("Retrieved User with Email {Email}", query.Email);
            return _mapper.Map<UserDto>(user);
        }
    }
}
