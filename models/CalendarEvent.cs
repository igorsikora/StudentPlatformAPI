using System;
using StudentPlatformAPI.Models.Auth;

namespace StudentPlatformAPI.Models
{
    public class CalendarEvent
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }

        public void SetUserId(Guid userId)
        {
            UserId = userId;
        }
    }
}