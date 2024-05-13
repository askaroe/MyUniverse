using MyUniverse.Dtos;
using MyUniverse.Models;

namespace MyUniverse.Services.Student
{
    public interface IStudentService
    {
        Task<StudentModel> Register(StudentModel newStudent);
        Task<List<CourseModel>> ShowCourses(int studentId);
        Task<CourseModel?> ShowCourse(int courseId, int studentid);
        Task<string> UpdateProfile(int studnetId, UpdateDto updatedProfile);
        Task<QaModel> AskQuestion(int studentId, int receiverId, string question);
        Task<string> AnswerQuestion(int questionId, string answer);
        Task<List<QaModel>> GetAllQuestions();
        Task<string> DownloadFiles();
    }
}
