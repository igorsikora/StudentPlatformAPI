using System.Collections;
using System.Collections.Generic;

namespace StudentPlatformAPI.models
{
    public class Student
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public ICollection<CalendarEvent> CalendarEvents { get; set; }
        public ICollection<Task> Tasks { get; set; }
    }
}