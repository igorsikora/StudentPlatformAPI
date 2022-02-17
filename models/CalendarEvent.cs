using System;
using System.ComponentModel.DataAnnotations;
using StudentPlatformAPI.Models.Auth;

namespace StudentPlatformAPI.Models
{
    public class CalendarEvent
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        [Required]
        public Guid UserId { get; set; }
        public User User { get; set; }

        public void SetUserId(Guid userId)
        {
            UserId = userId;
        }
    }
}