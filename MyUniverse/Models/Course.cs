using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyUniverse.Models
{
    [Table("Courses")]
    public class Course
    {
        [Key]
        [Column("Course_ID")]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
    }
}
