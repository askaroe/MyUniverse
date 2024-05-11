using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyUniverse.Models
{
    [Table("Student_Courses")]
    public class StudentCourseModel
    {
        [Key]
        [Column("Student_course_ID")]
        public int Id { get; set; }
        [Column("Student_ID")]
        [ForeignKey("Student")]
        public int StudentId { get; set; }
        [Column("Course_ID")]
        [ForeignKey("Course")]
        public int CourseId { get; set; }
        public string? Status { get; set; }

        public StudentModel Student { get; set; }
        public CourseModel Course { get; set; }
    }
}
