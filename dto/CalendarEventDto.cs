﻿using System;

namespace StudentPlatformAPI.dto
{
    public class CalendarEventDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public int StudentId { get; set; }

    }
}