using App.Application.Queries.User.GetUser.ById;
using App.Application.Validators.Exceptions;
using App.Domain.Abstractions;
using App.Domain.DTOs;
using AutoMapper;
using MediatR;

namespace App.Application.Queries.User.GetUser.ByUsername
{
    public class GetUserByUsernameHandler : IRequestHandler<GetUserByUsernameQuery, UserDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetUserByUsernameHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<UserDto> Handle(GetUserByUsernameQuery query, CancellationToken cancellation)
        {
            var user = await _unitOfWork.Users.GetUserByUsernameAsync(query.Username);

            if (user == null)
            {
                throw new NotFoundException("User", query.Username);
            }

            return _mapper.Map<UserDto>(user);
        }
    }
}
