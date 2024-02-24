using AutoMapper;
using ToDoList.Application.Dto.User;
using ToDoList.Domain.Entity;

namespace ToDoList.Application.Mapping
{
    public class UserMapping : Profile
    {
        public UserMapping()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, UserProfileDto>().ReverseMap();
        }
    }
}
