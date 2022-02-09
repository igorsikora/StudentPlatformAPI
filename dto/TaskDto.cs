using StudentPlatformAPI.models;

namespace StudentPlatformAPI.dto
{
    public class TaskDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int StatusId { get; set; }

        public int StudentId { get; set; }

    }
}