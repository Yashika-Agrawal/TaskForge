using AutoMapper;
using TaskForge.Application.DTOs;
using TaskForge.Domain.Entities;

namespace TaskForge.Application.Mappings
{
    public class UsersProfile : Profile
    {
        public UsersProfile()
        {
            CreateMap<RegisterUserDto, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.Password)); //RegisterUserDto to User

            CreateMap<User, UserDto>() //dest is UserDto
                .ForMember(dest => dest.Roles, 
                opt => opt.MapFrom(src => src.UserRoles.Select(ur => ur.Role)));
        }
    }
}
