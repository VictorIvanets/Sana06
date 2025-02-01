using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MF.Models.ViewModels
{
    public class FishingSessionVM
    {
        //public int Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public DateTime Date { get; set; }
        public int Rating { get; set; }
        public required string UserName { get; set; }
        public required string PlaceName { get; set; }
        public required string PlaceDescription { get; set; }
        public required string Lat  { get; set; } 
        public required string Lng { get; set; }
        public required string temp { get; set; }
        public required string pressure { get; set; }
        public required string humidity { get; set; }
        public required string speed { get; set; }
        public required string deg { get; set; }
        public required string sky { get; set; }

    }
}

