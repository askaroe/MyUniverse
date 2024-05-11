using MyUniverse.Dtos;
using MyUniverse.Models;
using MyUniverse.Responses;

namespace MyUniverse.Services.Admin
{
    public interface IAdminService
    {
        Task<List<StudentModel>> GetStudentList();
        Task<List<TeacherModel>> GetTeacherList();
        Task<(string?, StudentModel?)> GetStudentById(int studentId);
        Task<(string?, TeacherModel?)> GetTeacherById(int teacherId);
        Task<string> AddStudent(StudentModel studentModel);
        Task<string> AddTeacher(TeacherModel teacherModel);
        Task<StudentModel?> UpdateStudent(int studentId, UpdateDto newStudent);
        Task<TeacherModel?> UpdateTeacher(int studentId, UpdateDto newTeacher);
        Task<string> DeleteStudent(int studentId);
        Task<string> DeleteTeacher(int teacherId);
        Task<List<CourseModel>> GetCourseList();
        Task<(string?, CourseModel?)> GetCourseById(int courseId);
        Task<string> AddCourse(CourseModel courseModel);
        Task<CourseModel?> UpdateCourse(int id, CourseModel newCourse);
        Task<string> DeleteCourse(int courseId);
        Task<string> AssignTeacherToCourse(int courseId, int teacherId);
        Task<string> AddStudentToCourse(int courseId, int studentId);
        Task<string> UpdateTeacherOnCourse(int courseId, int teacherId);
        Task<string> RemoveStudentFromCourse(int courseId, int studentId);
        Task<string> RemoveCourseFromUsers(int  courseId, int teacherId);
        Task<(string?, CourseTeacherStudents?)> GetCourseGroupInfo(int courseId, int teacherId);
    }
}
