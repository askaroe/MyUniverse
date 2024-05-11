using MyUniverse.Models;

namespace MyUniverse.Responses
{
    public class CourseTeacherStudents
    {
        public string CourseName { get; set; }
        public string TeacherName { get; set; }
        public List<StudentModel> Students { get; set; }
    }
}
