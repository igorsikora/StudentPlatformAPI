using System;
using System.Collections.Generic;
using StudentPlatformAPI.dto;

namespace StudentPlatformAPI.services
{
    public interface ICalendarEventService
    {
        public IEnumerable<CalendarEventDto> getMonthlyCalendarEvents(DateTime dateTime, int studentId);
        public IEnumerable<CalendarEventDto> getWeeklyCalendarEvents(IEnumerable<DateTime> dates, int studentId);

        public void updateCalendarEvent(CalendarEventDto dto);
        public int createCalendarEvent(CalendarEventDto dto);
        public void deleteCalendarEvent(int id);

    }
}