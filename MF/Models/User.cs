using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace MF.Models
{
    public class User
    {
        [Key]
        public int id { get; set; }
        [Required]
        public string UserName   { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string password { get; set; }


        [Display(Name = "Role")]
        public int roleId { get; set; }

        [ForeignKey("roleId")]
        public virtual Roles Roles { get; set; }

    }
}