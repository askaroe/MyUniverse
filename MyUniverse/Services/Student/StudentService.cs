using Microsoft.EntityFrameworkCore;
using MyUniverse.Data;
using MyUniverse.Dtos;
using MyUniverse.Models;
using MyUniverse.Responses;

namespace MyUniverse.Services.Student
{
    public class StudentService : IStudentService
    {
        private readonly ApplicationContext _context;

        public StudentService(ApplicationContext context)
        {
            _context = context;
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

        public async Task<(string?, QuestionAnswerWithNames?)> AskQuestion(int studentId, int receiverId, string question)
        {
            var studentName = await _context.Students
               .Where(s => s.Id == studentId)
               .Select(s => s.Name)
               .FirstOrDefaultAsync();

            if (studentName == null)
            {
                return ($"Student with id: {studentId} does not exist", null);
            }

            var teacherName = await _context.Teachers
                .Where(t => t.Id == receiverId)
                .Select(t => t.Name)
                .FirstOrDefaultAsync();

            if (teacherName == null)
            {
                return ($"Teacher with id: {receiverId} does not exist", null);
            }

            var qaModel = new QaModel
            {
                StudentId = studentId,
                TeacherId = receiverId,
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

        public async Task<string> DownloadFiles()
        {
            await Task.Delay(1000);

            return "File downloaded";
        }

        public async Task<List<QuestionAnswerWithNames>> GetAllQuestions()
        {
            return await _context.Qas
                .Include(q => q.Student)
                .Include(q => q.Teacher)
                .Select(q => new QuestionAnswerWithNames
                {
                    StudentName = q.Student.Name,
                    TeacherName = q.Teacher.Name,
                    Question = q.QuestionDescription,
                    Answer = q.Answer ?? "wating for response"
                })
                .ToListAsync();
        }

        public async Task<StudentModel> Register(StudentModel newStudent)
        {
            await _context.Students.AddAsync(newStudent);
            await _context.SaveChangesAsync();
            return newStudent;
        }

        public async Task<StudentCourseWithMark?> ShowCourse(int courseId, int studentid)
        {
            var studentCourse = await _context.StudentCourses
                .Include(sc => sc.Course)
                .FirstOrDefaultAsync(sc => sc.CourseId == courseId && sc.StudentId == studentid);
                
            if(studentCourse == null)
            {
                return null;
            }

            return new StudentCourseWithMark
            {
                Course = studentCourse.Course,
                Status = studentCourse.Status
            };
        }

        public async Task<List<CourseModel>> ShowCourses(int studentId)
        {
            return await _context.StudentCourses
                .Include(sc => sc.Course)
                .Where(sc => sc.StudentId == studentId)
                .Select(sc => sc.Course)
                .ToListAsync();
        }

        public async Task<string> UpdateProfile(int studnetId, UpdateDto updatedProfile)
        {
            var student = await _context.Students.FirstOrDefaultAsync(s => s.Id == studnetId);
            if(student == null)
            {
                return $"Student with id: {studnetId} was not found";
            }

            student.Name = updatedProfile.Name;
            student.Surname = updatedProfile.Surname;
            student.PhoneNumber = updatedProfile.PhoneNumber;
            student.Email = updatedProfile.Email;
            student.Password = updatedProfile.Password;

            _context.Students.Update(student);
            await _context.SaveChangesAsync();

            return "Profile updated successfully";
        }
    }
}
