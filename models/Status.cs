using System.Collections;
using System.Collections.Generic;

namespace StudentPlatformAPI.models
{
    public enum Statuses
    {
        ToDo,
        InProgress,
        Done
}

    public class Status
    {
        public Statuses Id { get; set; }
        public string Name { get; set; }
    }
}