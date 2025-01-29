using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MF.Models
{
    public class PlaceDb
    {
        [Key]
        public int id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }
        [Required]


        [Display(Name = "Coords")]
        public int CoordsId { get; set; }

        [ForeignKey("CoordsId")]
        public required virtual Coords Coords { get; set; }
    }
}
