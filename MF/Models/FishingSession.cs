using MF.Controllers;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MF.Models
{
    public class FishingSession
    {

        [Key]
        public int Id { get; set; }

        public required string  Name { get; set; }

        public required string Description { get; set; }

        public DateTime Date { get; set; }

        public int Rating { get; set; }



        [Display(Name = "Weather")]
        public int WeatherId { get; set; }
        [ForeignKey("WeatherId")]
        public required virtual Weather Weather { get; set; }


        [Display(Name = "Place")]
        public int PlaceId { get; set; }
        [ForeignKey("PlaceId")]
        public required virtual PlaceDb PlaceDb { get; set; }


        [Display(Name = "User")]
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public required virtual AppUser ApplicationUser { get; set; }
    }
}
