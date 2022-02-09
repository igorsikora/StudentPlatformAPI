using System;
using System.Collections.Generic;
using StudentPlatformAPI.data;
using StudentPlatformAPI.dto;
using System.Linq;
using AutoMapper;
using StudentPlatformAPI.models;


namespace StudentPlatformAPI.services
{
    public class CalendarEventService : ICalendarEventService
    {
        private StudentPlatformContext _context;
        private IMapper _mapper;
        public CalendarEventService(StudentPlatformContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IEnumerable<CalendarEventDto> getMonthlyCalendarEvents(DateTime dateTime, int studentId)
        {
            return _context.CalendarEvents.Where(ce => ce.StartDate.Month == dateTime.Month && ce.StudentId == studentId).Select(ce => new CalendarEventDto()
            {
                Description = ce.Description,
                EndDate = ce.EndDate,
                Id = ce.Id,
                StudentId = ce.StudentId,
                StartDate = ce.StartDate,
                Title = ce.Title
            }).ToList();
        }

        public IEnumerable<CalendarEventDto> getWeeklyCalendarEvents(IEnumerable<DateTime> dates, int studentId)
        {
            DateTime firstDate = new DateTime(dates.First().Year, dates.First().Month, dates.First().Day);
            DateTime lastDate = new DateTime(dates.Last().Year, dates.Last().Month, dates.Last().Day, 23, 59, 59); // end date as last possible time
            return _context.CalendarEvents.Where(ce => ce.StartDate >= firstDate && ce.StartDate <= lastDate && ce.StudentId == studentId).Select(ce => new CalendarEventDto()
            {
                Description = ce.Description,
                EndDate = ce.EndDate,
                Id = ce.Id,
                StudentId = ce.StudentId,
                StartDate = ce.StartDate,
                Title = ce.Title
            }).ToList();
        }

        public void updateCalendarEvent(CalendarEventDto dto)
        {
            var calendarEvent = _context.CalendarEvents.FirstOrDefault(ce => ce.Id == dto.Id);
            if(!string.IsNullOrEmpty(dto.Description)) calendarEvent.Description = dto.Description;
            calendarEvent.StartDate = dto.StartDate;
            calendarEvent.EndDate = dto.EndDate;
            calendarEvent.Title = dto.Title;

            _context.SaveChanges();
        }

        public int createCalendarEvent(CalendarEventDto dto)
        {
            CalendarEvent calendarEvent = _mapper.Map<CalendarEvent>(dto);
            _context.CalendarEvents.Add(calendarEvent);
            _context.SaveChanges();
            return calendarEvent.Id;
        }

        public void deleteCalendarEvent(int id)
        {
            var calendarEvent = _context.CalendarEvents.FirstOrDefault(ce => ce.Id == id);

            if (calendarEvent != null)
            {
                _context.CalendarEvents.Remove(calendarEvent);
                _context.SaveChanges();

            }
        }
    }
}