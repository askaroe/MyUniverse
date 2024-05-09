using Microsoft.EntityFrameworkCore;
using MyUniverse.Data;
using MyUniverse.Dtos;
using MyUniverse.Models;

namespace MyUniverse.Services.Student
{
    public class StudentService : IStudentService
    {
        private readonly ApplicationContext _context;

        public StudentService(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<List<StudentModel>> GetAllStudents()
        {
            return await _context.Students.ToListAsync();
        }

        public async Task<StudentModel?> GetStudentByEmail(string email)
        {
            var student = await _context.Students.FirstOrDefaultAsync(x => x.Email == email);
            if (student == null)
            {
                return null;
            }
            return student;
        }

        public async Task<StudentModel> Register(StudentModel newStudent)
        {
            await _context.Students.AddAsync(newStudent);
            await _context.SaveChangesAsync();
            return newStudent;
        }

        public async Task<CourseModel?> ShowCourse(int courseId, int studentid)
        {
            var studentCourse = await _context.StudentCourses
                .Include(sc => sc.Course)
                .FirstOrDefaultAsync(sc => sc.CourseId == courseId && sc.StudentId == studentid);
                
            if(studentCourse == null)
            {
                return null;
            }

            return studentCourse.Course;
        }

        public async Task<List<CourseModel>> ShowCourses(int studentId)
        {
            return await _context.StudentCourses
                .Include(sc => sc.Course)
                .Where(sc => sc.StudentId == studentId)
                .Select(sc => sc.Course)
                .ToListAsync();
        }
    }
}
