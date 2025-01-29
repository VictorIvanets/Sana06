using System.ComponentModel.DataAnnotations;

namespace MF.Models
{
    public class Coords
    {
        [Key]
        public int id { get; set; }
        public required string Lat {  get; set; }
        public required string Long { get; set; }

    }
}
