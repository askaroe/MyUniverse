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

        public async Task<string> AnswerQuestion(int questionId, string answer)
        {
            var qaModel = await _context.Qas.FirstOrDefaultAsync(q => q.Id == questionId);
            if (qaModel == null)
            {
                return $"The question with id: {questionId} does not exist";
            }

            qaModel.Answer = answer;
            await _context.SaveChangesAsync();
            return "Answer was saved successfully";
        }

        public async Task<QaModel> AskQuestion(int studentId, int receiverId, string question)
        {
            var qaModel = new QaModel
            {
                StudentId = studentId,
                TeacherId = receiverId,
                QuestionDescription = question,
                Answer = "waiting for answer..."
            };

            await _context.Qas.AddAsync(qaModel);
            await _context.SaveChangesAsync();
            return qaModel;
        }

        public async Task<string> DownloadFiles()
        {
            await Task.Delay(1000);

            return "File downloaded";
        }

        public async Task<List<QaModel>> GetAllQuestions()
        {
            return await _context.Qas.ToListAsync();
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

        public Task<string> UpdateProfile(int studnetId, UpdateDto updatedProfile)
        {
            throw new NotImplementedException();
        }
    }
}
