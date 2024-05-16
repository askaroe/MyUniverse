using MyUniverse.Dtos;
using MyUniverse.Models;
using MyUniverse.Responses;

namespace MyUniverse.Services.Teacher
{
    public interface ITeacherService
    {
        Task<string> UpdateProfile(int id, UpdateDto updateDto);
        Task<(string?, QuestionAnswerWithNames?)> AnswerQuestion(int questionId, string answer);
        Task<string> DownloadFiles();
        Task<(string?, QuestionAnswerWithNames?)> AddQuestion(int teacherId, int receiverId, string question);
        Task<TeacherModel> Register(TeacherModel newTeacher);
        Task<List<QuestionsForTeacherWithId>> GetQuestionsForTeacher(int teacherId);
    }
}
