using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace StudentPlatformAPI.Models.Auth
{
    public class User : IdentityUser<Guid>
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }

        public ICollection<CalendarEvent> CalendarEvents { get; set; }
        public ICollection<Task> Tasks { get; set; }

    }
}