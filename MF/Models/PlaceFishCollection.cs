using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MF.Models
{
    public class PlaceFishCollection
    {
        [Key]
        public int id { get; set; }


        [Display(Name = "Fishes")]
        public int FishesId { get; set; }

        [ForeignKey("FishesId")]
        public required virtual Fishes Fishes { get; set; }


        [Display(Name = "Place")]
        public int PlaceDbId { get; set; }

        [ForeignKey("PlaceDbId")]
        public required virtual PlaceDb PlaceDb { get; set; }
    }
}
