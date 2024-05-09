using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyUniverse.Models
{
    [Table("Students")]
    public class StudentModel : UserModel
    {
        [Column("Student_ID")]
        public override int Id {  get; set; }
    }
}
