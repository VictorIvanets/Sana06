using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MF.Models
{
    public class FishPlaceCollection
    {
        [Key]
        public int Id { get; set; }


        [Display(Name = "Place")]
        public int PlaceId { get; set; }
        [ForeignKey("PlaceId")]
        public virtual PlaceDb PlaceDb { get; set; }


        [Display(Name = "Fishes")]
        public int FishesId { get; set; }
        [ForeignKey("FishesId")]
        public virtual Fishes Fishes { get; set; }
    }
}
