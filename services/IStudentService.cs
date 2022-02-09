using System.Linq;
using StudentPlatformAPI.dto;

namespace StudentPlatformAPI.services
{
    public interface IStudentService
    {
        public StudentDto getStudent(int studentId);
        public void updateStudent(StudentDto dto);

    }
}