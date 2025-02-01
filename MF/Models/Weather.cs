using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MF.Models
{
    public class Weather
    {
        [Key]
        public int Id { get; set; }
        public string Temp { get; set; }
        public string Pressure { get; set; }
        public string Humidity { get; set; }
        public string Windspeed { get; set; }
        public string Winddeg { get; set; }
        public string Sky { get; set; }

    }
}


