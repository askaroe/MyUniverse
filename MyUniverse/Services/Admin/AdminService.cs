using Microsoft.EntityFrameworkCore;
using MyUniverse.Data;
using MyUniverse.Dtos;
using MyUniverse.Models;
using MyUniverse.Responses;

namespace MyUniverse.Services.Admin
{
    public class AdminService : IAdminService
    {
        private readonly ApplicationContext _context;

        public AdminService(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<string> AddCourse(CourseModel courseModel)
        {
            await _context.Courses.AddAsync(courseModel);
            await _context.SaveChangesAsync();
            return "Course added successfully!";
        }

        public async Task<string> AddStudent(StudentModel studentModel)
        {
            await _context.Students.AddAsync(studentModel);
            await _context.SaveChangesAsync();
            return "Student added successfully!";
        }

        public async Task<string> AddTeacher(TeacherModel teacherModel)
        {
            await _context.Teachers.AddAsync(teacherModel);
            await _context.SaveChangesAsync();
            return "Teacher added successfully!";
        }

        public async Task<string> DeleteCourse(int courseId)
        {
            var course = await _context.Courses.FirstOrDefaultAsync(c => c.Id == courseId);
            if (course == null)
            {
                return $"Course with id: {courseId} was not found";
            }
            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
            return "Course deleted successfully";
        }

        public async Task<string> DeleteStudent(int studentId)
        {
            var student = await _context.Students.FirstOrDefaultAsync(s => s.Id == studentId);
            if (student == null)
            {
                return $"Student with id: {studentId} was not found";
            }
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
            return "Student deleted successfully";
        }

        public async Task<string> DeleteTeacher(int teacherId)
        {
            var teacher = await _context.Teachers.FirstOrDefaultAsync(t => t.Id == teacherId);
            if (teacher == null)
            {
                return $"Teacher with id: {teacherId} was not found";
            }
            _context.Teachers.Remove(teacher);
            await _context.SaveChangesAsync();
            return "Teacher deleted successfully";
        }

        public async Task<(string?, CourseModel?)> GetCourseById(int courseId)
        {
            var course = await _context.Courses.FirstOrDefaultAsync(c => c.Id == courseId);
            if(course == null)
            {
                return ($"Course with id: {courseId} does not exist", null);
            }
            return (null, course);
        }

        public async Task<List<CourseModel>> GetCourseList()
        {
            return await _context.Courses.ToListAsync();
        }

        public async Task<(string?, StudentModel?)> GetStudentById(int studentId)
        {
            var student = await _context.Students.FirstOrDefaultAsync(s => s.Id == studentId);
            if (student == null)
            {
                return ($"Student with id: {studentId} does not exist", null);
            }
            return (null, student);
        }

        public async Task<List<StudentModel>> GetStudentList()
        {
            return await _context.Students.ToListAsync();
        }

        public async Task<(string?, TeacherModel?)> GetTeacherById(int teacherId)
        {
            var teacher = await _context.Teachers.FirstOrDefaultAsync(t => t.Id == teacherId);
            if (teacher == null)
            {
                return ($"Teacher with id: {teacherId} does not exist", null);
            }
            return (null, teacher);
        }

        public async Task<List<TeacherModel>> GetTeacherList()
        {
            return await _context.Teachers.ToListAsync();
        }

        public async Task<CourseModel?> UpdateCourse(int id, CourseModel newCourse)
        {
            var course = await _context.Courses.FirstOrDefaultAsync(c => c.Id == id);
            if (course == null) 
            {
                return null;
            }
            
            course.Name = newCourse.Name;
            course.Description = newCourse.Description;
            course.Type = newCourse.Type;
            _context.Courses.Update(course);
            await _context.SaveChangesAsync();
            return course;
        }

        public async Task<StudentModel?> UpdateStudent(int studentId, UpdateDto newStudent)
        {
            var student = await _context.Students.FirstOrDefaultAsync(s => s.Id == studentId);
            if (student == null)
            {
                return null;
            }

            student.Name = newStudent.Name;
            student.Surname = newStudent.Surname;
            student.Email = newStudent.Email;
            student.Password = newStudent.Password;
            _context.Students.Update(student);
            await _context.SaveChangesAsync();
            return student;
        }

        public async Task<TeacherModel?> UpdateTeacher(int studentId, UpdateDto newTeacher)
        {
            var teacher = await _context.Teachers.FirstOrDefaultAsync(t => t.Id == studentId);
            if (teacher == null)
            {
                return null;
            }
            teacher.Name = newTeacher.Name;
            teacher.Surname = newTeacher.Surname;
            teacher.Email = newTeacher.Email;
            teacher.Password = newTeacher.Password;
            _context.Teachers.Update(teacher);
            await _context.SaveChangesAsync();
            return teacher;
        }

        public async Task<string> AssignTeacherToCourse(int courseId, int teacherId)
        {
            var course = await _context.Courses.FirstOrDefaultAsync(c => c.Id == courseId);
            var teacher = await _context.Teachers.FirstOrDefaultAsync(t => t.Id == teacherId);

            if (course == null)
            {
                return $"Course with id: {courseId} does not exist";
            }

            if (teacher == null)
            {
                return $"Teacher with id: {teacherId} does not exist";
            }

            var existingEnrollment = await _context.TeacherCourses
                    .FirstOrDefaultAsync(tc => tc.CourseId == courseId);
            
            if (existingEnrollment != null)
            {
                return "Teacher is already exist in this course";
            }

            var teacherCourse = new TeacherCourseModel
            {
                CourseId = courseId,
                TeacherId = teacherId,
            };

            await _context.TeacherCourses.AddAsync(teacherCourse);
            await _context.SaveChangesAsync();

            return "Teacher added successfully";
        }

        public async Task<string> AddStudentToCourse(int courseId, int studentId)
        {
            var course = await _context.Courses.FirstOrDefaultAsync(c => c.Id == courseId);
            var student = await _context.Students.FirstOrDefaultAsync(s => s.Id == studentId);

            if (course == null)
            {
                return $"Course with id: {courseId} does not exist";
            }

            if (student == null)
            {
                return $"Teacher with id: {studentId} does not exist";
            }

            var existingEnrollment = await _context.StudentCourses
                    .FirstOrDefaultAsync(sc => sc.CourseId == courseId && sc.StudentId == studentId);

            if (existingEnrollment != null)
            {
                return "Student is already exist in this course";
            }

            var studentCourse = new StudentCourseModel
            {
                CourseId = courseId,
                StudentId = studentId
            };

            await _context.StudentCourses.AddAsync(studentCourse);
            await _context.SaveChangesAsync();

            return "Student added successfully";
        }

        public async Task<string> UpdateTeacherOnCourse(int courseId, int teacherId)
        {
            var teacherCourse = await _context.TeacherCourses.FirstOrDefaultAsync(tc => tc.CourseId == courseId);

            if(teacherCourse == null)
            {
                return $"Teacher course with id: {courseId} does not exist";
            }

            teacherCourse.TeacherId = teacherId;

            _context.TeacherCourses.Update(teacherCourse);
            await _context.SaveChangesAsync();

            return "Teaacher course updated successfully";
        }

        public async Task<string> RemoveStudentFromCourse(int courseId, int studentId)
        {
            var studentCourse = await _context.StudentCourses
                .FirstOrDefaultAsync(sc => sc.CourseId == courseId && sc.StudentId == studentId);

            if (studentCourse == null)
            {
                return $"Student course with id: {courseId} does not exist";
            }

            _context.StudentCourses.Remove(studentCourse);
            await _context.SaveChangesAsync();

            return "Student removed from course successfully";
        }

        public async Task<string> RemoveCourseFromUsers(int courseId, int teacherId)
        {
            var teacherCourse = await _context.TeacherCourses
                .FirstOrDefaultAsync(tc => tc.CourseId == courseId && tc.TeacherId == teacherId);

            var studentsCourse = await _context.StudentCourses
                .Where(sc => sc.CourseId == courseId)
                .ToListAsync();

            if (studentsCourse.Count == 0)
            {
                return $"Course with id: {courseId} have no students";
            }

            if (teacherCourse == null)
            {
                return $"Teacher id: {teacherId} with course id: {courseId} does not exist";
            }

            _context.TeacherCourses.Remove(teacherCourse);
            _context.StudentCourses.RemoveRange(studentsCourse);
            await _context.SaveChangesAsync();

            return "Course removed from users successfully";
        }

        public async Task<(string?, CourseTeacherStudents?)> GetCourseGroupInfo(int courseId, int teacherId)
        {
            var teacherCourse = await _context.TeacherCourses
                .Include(tc => tc.Teacher)
                .Include(tc => tc.Course)
                .FirstOrDefaultAsync(tc => tc.CourseId == courseId && tc.TeacherId == teacherId);

            var studentsOfCourse = await _context.StudentCourses
                .Include(sc => sc.Student)
                .Where(sc => sc.CourseId == courseId)
                .Select(sc => sc.Student)
                .ToListAsync();

            if (teacherCourse == null)
            {
                return ($"There is no course with id {courseId} for this teacher", null);
            }

            if (studentsOfCourse.Count == 0)
            {
                return ($"There are no students enrolled in the course with id: {courseId}", null);
            }

            return (null, new CourseTeacherStudents
            {
                CourseName = teacherCourse.Course.Name,
                TeacherName = teacherCourse.Teacher.Name,
                Students = studentsOfCourse
            });
        }


    }
}
