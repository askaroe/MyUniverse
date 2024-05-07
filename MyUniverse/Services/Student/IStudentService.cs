using MyUniverse.Dtos;
using MyUniverse.Models;

namespace MyUniverse.Services.Student
{
    public interface IStudentService
    {
        Task<StudentModel> Register(StudentModel newStudent);
        Task<StudentModel?> Login(LoginDto loginModel);
        Task<List<CourseModel>> ShowCourses(int studentId);
        Task<CourseModel?> ShowCourse(int courseId, int studentid);

    }
}
