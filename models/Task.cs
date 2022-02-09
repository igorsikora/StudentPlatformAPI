using System.ComponentModel.DataAnnotations.Schema;

namespace StudentPlatformAPI.models
{
    public class Task
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int StatusId { get; set; }

        public int StudentId { get; set; }
        public Student Student { get; set; }
    }
}