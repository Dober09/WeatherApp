using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.Models
{
    public class WeatherData
    {
        public string Name { get; set; } // City name
        public MainData Main { get; set; }
        public Weather[] Weather { get; set; }
        public WindData Wind { get; set; }
    }
}
