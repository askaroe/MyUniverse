using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyUniverse.Models
{
    [Table("Teachers")]
    public class Teacher
    {
        [Key]
        [Column("Teacher_ID")]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}
