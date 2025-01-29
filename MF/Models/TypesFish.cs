using System.ComponentModel.DataAnnotations;

namespace MF.Models
{
    public class TypesFish
    {
        [Key]
        public int id { get; set; }
        [Required]
        public string Type { get; set; }
    }
}
