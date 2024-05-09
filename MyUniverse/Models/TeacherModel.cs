using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyUniverse.Models
{
    [Table("Teachers")]
    public class TeacherModel : UserModel
    {
        [Column("Teacher_ID")]
        public override int Id { get; set; }
    }
}
