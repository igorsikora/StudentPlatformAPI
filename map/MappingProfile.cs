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
            CreateMap<UserSignUpDto, User>();
            CreateMap<UserUpdateDto, User>();
        }
    }
}