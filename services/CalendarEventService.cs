using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using StudentPlatformAPI.Data;
using StudentPlatformAPI.Dto;
using StudentPlatformAPI.Models;


namespace StudentPlatformAPI.Services
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

        public IEnumerable<CalendarEventDto> GetMonthlyCalendarEvents(DateTime dateTime, Guid userId)
        {
            return _context.CalendarEvents.Where(ce => ce.StartDate.Month == dateTime.Month && ce.UserId == userId).Select(ce => new CalendarEventDto()
            {
                Description = ce.Description,
                EndDate = ce.EndDate,
                Id = ce.Id,
                StartDate = ce.StartDate,
                Title = ce.Title
            }).OrderBy(ce => ce.StartDate).ToList();
        }

        public IEnumerable<CalendarEventDto> GetWeeklyCalendarEvents(IEnumerable<DateTime> dates, Guid userId)
        {
            DateTime firstDate = new DateTime(dates.First().Year, dates.First().Month, dates.First().Day);
            DateTime lastDate = new DateTime(dates.Last().Year, dates.Last().Month, dates.Last().Day, 23, 59, 59); // end date as last possible time
            return _context.CalendarEvents.Where(ce => ce.StartDate >= firstDate && ce.StartDate <= lastDate && ce.UserId == userId).Select(ce => new CalendarEventDto()
            {
                Description = ce.Description,
                EndDate = ce.EndDate,
                Id = ce.Id,
                StartDate = ce.StartDate,
                Title = ce.Title
            }).OrderBy(ce => ce.StartDate).ToList();
        }

        public void UpdateCalendarEvent(CalendarEventDto dto, Guid userId)
        {
            var calendarEvent = _context.CalendarEvents.FirstOrDefault(ce => ce.Id == dto.Id && ce.UserId == userId);
            if(!string.IsNullOrEmpty(dto.Description)) calendarEvent.Description = dto.Description;
            calendarEvent.StartDate = dto.StartDate;
            calendarEvent.EndDate = dto.EndDate;
            calendarEvent.Title = dto.Title;

            _context.SaveChanges();
        }

        public int CreateCalendarEvent(CalendarEventDto dto, Guid userId)
        {
            CalendarEvent calendarEvent = _mapper.Map<CalendarEvent>(dto);
            calendarEvent.SetUserId(userId);
            _context.CalendarEvents.Add(calendarEvent);
            _context.SaveChanges();
            return calendarEvent.Id;
        }

        public void DeleteCalendarEvent(int id, Guid userId)
        {
            var calendarEvent = _context.CalendarEvents.FirstOrDefault(ce => ce.Id == id && ce.UserId == userId);

            if (calendarEvent != null)
            {
                _context.CalendarEvents.Remove(calendarEvent);
                _context.SaveChanges();

            }
        }
    }
}