using MyUniverse.Dtos;
using MyUniverse.Models;
using MyUniverse.Responses;

namespace MyUniverse.Services.Student
{
    public interface IStudentService
    {
        Task<StudentModel> Register(StudentModel newStudent);
        Task<List<CourseModel>> ShowCourses(int studentId);
        Task<StudentCourseWithMark?> ShowCourse(int courseId, int studentid);
        Task<string> UpdateProfile(int studnetId, UpdateDto updatedProfile);
        Task<(string?, QuestionAnswerWithNames?)> AskQuestion(int studentId, int receiverId, string question);
        Task<(string?, QuestionAnswerWithNames?)> AnswerQuestion(int questionId, string answer);
        Task<List<QuestionAnswerWithNames>> GetAllQuestions();
        Task<string> DownloadFiles();
    }
}
