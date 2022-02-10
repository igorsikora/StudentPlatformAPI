using System;
using System.Collections.Generic;
using StudentPlatformAPI.Dto;

namespace StudentPlatformAPI.Services
{
    public interface ICalendarEventService
    {
        public IEnumerable<CalendarEventDto> getMonthlyCalendarEvents(DateTime dateTime, Guid userId);
        public IEnumerable<CalendarEventDto> getWeeklyCalendarEvents(IEnumerable<DateTime> dates, Guid userId);

        public void updateCalendarEvent(CalendarEventDto dto, Guid userId);
        public int createCalendarEvent(CalendarEventDto dto, Guid userId);
        public void deleteCalendarEvent(int id, Guid userId);

    }
}