using App.Application.Validators.Exceptions;
using App.Domain.Abstractions;
using App.Domain.DTOs;
using AutoMapper;
using MediatR;

namespace App.Application.Queries.User.GetUser.ById
{
    public class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, UserDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetUserByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<UserDto> Handle(GetUserByIdQuery query, CancellationToken cancellation)
        {
            var user = await _unitOfWork.Users.GetUserByIdAsync(query.UserId);

            if (user == null)
            {
                throw new NotFoundException("User", query.UserId);
            }

            return _mapper.Map<UserDto>(user);
        }
    }
}
