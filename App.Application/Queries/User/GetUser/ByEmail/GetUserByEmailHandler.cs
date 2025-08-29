using App.Application.Queries.User.GetUser.ById;
using App.Application.Validators.Exceptions;
using App.Domain.Abstractions;
using App.Domain.DTOs;
using AutoMapper;
using MediatR;

namespace App.Application.Queries.User.GetUser.ByEmail
{
    public class GetUserByEmailHandler : IRequestHandler<GetUserByEmailQuery, UserDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetUserByEmailHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<UserDto> Handle(GetUserByEmailQuery query, CancellationToken cancellation)
        {
            var user = await _unitOfWork.Users.GetUserByEmailAsync(query.Email, cancellation);

            if (user == null)
            {
                throw new NotFoundException("User", query.Email);
            }

            return _mapper.Map<UserDto>(user);
        }
    }
}
