using App.Application.Commands.Users.AddUser;
using App.Application.Commands.Users.UpdateUser;
using App.Domain.DTOs;
using ToDo.Domain.Entities;
using AutoMapper;

namespace App.Application.Mappings
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
