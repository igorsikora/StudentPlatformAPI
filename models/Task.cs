using System;
using StudentPlatformAPI.Models.Auth;

namespace StudentPlatformAPI.Models
{
    public class Task
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int StatusId { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }

        public void SetUserId(Guid userId)
        {
            UserId = userId;
        }
    }
}