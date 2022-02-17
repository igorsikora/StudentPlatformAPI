using System;
using System.Collections.Generic;
using StudentPlatformAPI.Dto;

namespace StudentPlatformAPI.Services
{
    public interface ICalendarEventService
    {
        public IEnumerable<CalendarEventDto> GetMonthlyCalendarEvents(DateTime dateTime, Guid userId);
        public IEnumerable<CalendarEventDto> GetWeeklyCalendarEvents(IEnumerable<DateTime> dates, Guid userId);

        public void UpdateCalendarEvent(CalendarEventDto dto, Guid userId);
        public int CreateCalendarEvent(CalendarEventDto dto, Guid userId);
        public void DeleteCalendarEvent(int id, Guid userId);

    }
}