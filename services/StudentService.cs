using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using StudentPlatformAPI.data;
using StudentPlatformAPI.dto;
using StudentPlatformAPI.models;

namespace StudentPlatformAPI.services
{
    public class StudentService : IStudentService
    {
        private StudentPlatformContext _context;
        public StudentService(StudentPlatformContext context)
        {
            _context = context;
        }

        public StudentDto getStudent(int studentId)
        {
            return _context.Students.Where(s => s.Id == studentId).Select(s => new StudentDto
            {
                Email = s.Email, 
                FirstName = s.FirstName,
                LastName = s.LastName,
                Id = s.Id
            }).SingleOrDefault();
        }

        public void updateStudent(StudentDto dto)
        {
            var student = _context.Students.FirstOrDefault(s => s.Id == dto.Id);
            student.Email = dto.Email;
            student.FirstName = dto.FirstName;
            student.LastName = dto.LastName;

            _context.SaveChanges();
        }
    }
}