using MyUniverse.Models;

namespace MyUniverse.Responses
{
    public class StudentCourseWithMark
    {
        public CourseModel Course { get; set; }
        public string? Status { get; set; }
    }
}
