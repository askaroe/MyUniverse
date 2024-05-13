using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyUniverse.Models
{
    [Table("QuestionsAnswers")]
    public class QaModel
    {
        [Key]
        [Column("Question_ID")]
        public int Id { get; set; }
        [Column("Student_ID")]
        [ForeignKey("Student")]
        public int StudentId { get; set; }
        [Column("Teacher_ID")]
        [ForeignKey("Teacher")]
        public int TeacherId { get; set; }
        public string QuestionDescription { get; set; }
        public string? Answer { get; set; }

        public StudentModel Student { get; set; }
        public TeacherModel Teacher { get; set; }
    }
}
