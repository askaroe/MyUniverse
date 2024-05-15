using Microsoft.EntityFrameworkCore;
using MyUniverse.Data;
using MyUniverse.Dtos;
using MyUniverse.Models;
using MyUniverse.Responses;

namespace MyUniverse.Services.Teacher
{
    public class TeacherService : ITeacherService
    {
        private readonly ApplicationContext _context;

        public TeacherService(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<(string?, QuestionAnswerWithNames?)> AddQuestion(int teacherId, int receiverId, string question)
        {
            var studentName = await _context.Students
               .Where(s => s.Id == receiverId)
               .Select(s => s.Name)
               .FirstOrDefaultAsync();

            if (studentName == null)
            {
                return ($"Student with id: {receiverId} does not exist", null);
            }

            var teacherName = await _context.Teachers
                .Where(t => t.Id == teacherId)
                .Select(t => t.Name)
                .FirstOrDefaultAsync();

            if (teacherName == null)
            {
                return ($"Teacher with id: {teacherId} does not exist", null);
            }

            var qaModel = new QaModel
            {
                StudentId = receiverId,
                TeacherId = teacherId,
                QuestionDescription = question,
                Answer = "waiting for answer..."
            };

            await _context.Qas.AddAsync(qaModel);
            await _context.SaveChangesAsync();

            return (null, new QuestionAnswerWithNames
            {
                StudentName = studentName,
                TeacherName = teacherName,
                Question = question,
                Answer = "waiting for answer..."
            });
        }

        public async Task<(string?, QuestionAnswerWithNames?)> AnswerQuestion(int questionId, string answer)
        {
            var qaModel = await _context.Qas.FirstOrDefaultAsync(q => q.Id == questionId);
            if (qaModel == null)
            {
                return ($"The question with id: {questionId} does not exist", null);
            }

            qaModel.Answer = answer;
            await _context.SaveChangesAsync();

            var studentName = await _context.Students
               .Where(s => s.Id == qaModel.StudentId)
               .Select(s => s.Name)
               .FirstOrDefaultAsync();

            if (studentName == null)
            {
                return ($"Student with id: {qaModel.StudentId} does not exist", null);
            }

            var teacherName = await _context.Teachers
                .Where(t => t.Id == qaModel.TeacherId)
                .Select(t => t.Name)
                .FirstOrDefaultAsync();

            if (teacherName == null)
            {
                return ($"Teacher with id: {qaModel.TeacherId} does not exist", null);
            }

            return (null, new QuestionAnswerWithNames
            {
                StudentName = studentName,
                TeacherName = teacherName,
                Answer = answer,
                Question = qaModel.QuestionDescription
            });
        }

        public async Task<string> DownloadFiles()
        {
            await Task.Delay(1000);

            return "File downloaded";
        }

        public async Task<TeacherModel> Register(TeacherModel newTeacher)
        {
            await _context.Teachers.AddAsync(newTeacher);
            await _context.SaveChangesAsync();
            return newTeacher;
        }

        public async Task<string> UpdateProfile(int id, UpdateDto updateDto)
        {
            var teacher = await _context.Teachers.FirstOrDefaultAsync(s => s.Id == id);
            if (teacher == null)
            {
                return $"Student with id: {id} was not found";
            }

            teacher.Name = updateDto.Name;
            teacher.Surname = updateDto.Surname;
            teacher.PhoneNumber = updateDto.PhoneNumber;
            teacher.Email = updateDto.Email;
            teacher.Password = updateDto.Password;

            _context.Teachers.Update(teacher);
            await _context.SaveChangesAsync();

            return "Profile updated successfully";
        }
    }
}
