using System.ComponentModel.DataAnnotations;

namespace MF.Models
{
    public class Roles
    {
        [Key]
        public int id { get; set; }

        [Required]
        public string Role { get; set; }
    }
}
