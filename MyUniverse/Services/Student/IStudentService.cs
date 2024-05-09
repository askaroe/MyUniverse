using MyUniverse.Dtos;
using MyUniverse.Models;

namespace MyUniverse.Services.Student
{
    public interface IStudentService
    {
        Task<StudentModel> Register(StudentModel newStudent);
        Task<List<CourseModel>> ShowCourses(int studentId);
        Task<CourseModel?> ShowCourse(int courseId, int studentid);
        Task<StudentModel?> GetStudentByEmail(string email);
        Task<List<StudentModel>> GetAllStudents();
    }
}
