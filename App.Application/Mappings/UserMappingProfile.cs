using ToDo.Domain.Entities;
using AutoMapper;
using ToDo.Application.DTOs;
using ToDo.Application.Commands.Users.AddUser;
using ToDo.Application.Commands.Users.UpdateUser;

namespace ToDo.Application.Mappings
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<AddUserCommand, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore());

            CreateMap<UpdateUserCommand, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore());

            CreateMap<User, UserDto>();
        }
    }
}
