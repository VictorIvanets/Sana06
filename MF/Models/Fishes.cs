using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MF.Models
{
    public class Fishes
    {
        [Key]
        public int id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]

        [Display(Name = "Type Fish")]
        public int TypesFishId { get; set; }

        [ForeignKey("TypesFishId")]
        public virtual TypesFish TypesFish { get; set; }
    }
}
