using System;
using System.ComponentModel.DataAnnotations;
using StudentPlatformAPI.Models.Auth;

namespace StudentPlatformAPI.Models
{
    public class Task
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public Statuses Status { get; set; }
        [Required]
        public Guid UserId { get; set; }
        public User User { get; set; }

        public void SetUserId(Guid userId)
        {
            UserId = userId;
        }
    }
}