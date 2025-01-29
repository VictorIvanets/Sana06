using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MF.Models
{
    public class Place
    {
        public string Name { get; set; }
        
        public string Description { get; set; }

        public string Lat { get; set; }
       
        public string Long { get; set; }
    }
}
