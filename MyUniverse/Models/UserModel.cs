using System.ComponentModel.DataAnnotations;

namespace MyUniverse.Models
{
    public class UserModel
    {
        [Key]
        public virtual int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}
