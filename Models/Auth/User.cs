using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace StudentPlatformAPI.Models.Auth
{
    public class User : IdentityUser<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public ICollection<CalendarEvent> CalendarEvents { get; set; }
        public ICollection<Task> Tasks { get; set; }

    }
}