using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyUniverse.Models
{
    [Table("Teacher_Courses")]
    public class TeacherCourse
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

        public Teacher Teacher { get; set; }
        public Course Course { get; set; }
    }
}
