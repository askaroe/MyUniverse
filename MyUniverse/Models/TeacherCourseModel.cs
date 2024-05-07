using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyUniverse.Models
{
    [Table("Teacher_Courses")]
    public class TeacherCourseModel
    {
        [Key]
        [Column("Teacher_course_ID")]
        public int Id { get; set; }
        [ForeignKey("Teacher")]
        [Column("Teacher_ID")]
        public int TeacherId { get; set; }
        [ForeignKey("Course")]
        [Column("Course_ID")]
        public int CourseId { get; set; }

        public TeacherModel Teacher { get; set; }
        public CourseModel Course { get; set; }
    }
}
