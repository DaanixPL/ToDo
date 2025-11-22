using ToDo.Application.Validators.Exceptions;
using ToDo.Domain.Abstractions;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using ToDo.Application.DTOs;

namespace ToDo.Application.Queries.User.GetUser.ByUsername
{
    public class GetUserByUsernameHandler : IRequestHandler<GetUserByUsernameQuery, UserDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<GetUserByUsernameHandler> _logger;

        public GetUserByUsernameHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<GetUserByUsernameHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<UserDto> Handle(GetUserByUsernameQuery query, CancellationToken cancellation)
        {
            var user = await _unitOfWork.Users.GetUserByUsernameAsync(query.Username);

            if (user == null)
            {
                _logger.LogWarning("User with Username {Username} not found", query.Username);
                throw new NotFoundException("User", query.Username);
            }

            _logger.LogInformation("Retrieved User with Username {Username}", query.Username);
            return _mapper.Map<UserDto>(user);
        }
    }
}
