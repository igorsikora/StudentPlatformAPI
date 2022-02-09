using AutoMapper;
using StudentPlatformAPI.dto;
using StudentPlatformAPI.models;

namespace StudentPlatformAPI.map
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Student, StudentDto>();
            CreateMap<StudentDto, Student>();
            CreateMap<Task, TaskDto>();
            CreateMap<TaskDto, Task>();
            CreateMap<CalendarEvent, CalendarEventDto>();
            CreateMap<CalendarEventDto, CalendarEvent>();
        } 
    }
}