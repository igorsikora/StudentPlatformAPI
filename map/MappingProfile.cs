using AutoMapper;
using StudentPlatformAPI.Dto;
using StudentPlatformAPI.Models;
using StudentPlatformAPI.Models.Auth;

namespace StudentPlatformAPI.Map
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Task, TaskDto>();
            CreateMap<TaskDto, Task>();
            CreateMap<CalendarEvent, CalendarEventDto>();
            CreateMap<CalendarEventDto, CalendarEvent>();

            CreateMap<User, UserDto>();
            //Map for user register
            CreateMap<UserSignUpDto, User>()
                .ForMember(u => u.UserName, opt => opt.MapFrom(ur => ur.Email));
        } 
    }
}