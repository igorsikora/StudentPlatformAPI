using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentPlatformAPI.models
{
    public class CalendarEvent
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public int StudentId { get; set; }
        public Student Student { get; set; }
    }
}