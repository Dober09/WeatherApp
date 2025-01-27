
using System.Text.Json.Serialization;


namespace WeatherApp.Models
{
    public class WeatherData
    {
      
        public List<Weather> weather { get; set; }
        public string @base { get; set; }
        public MainData main { get; set; }
        public int visibility { get; set; }
        public WindData wind { get; set; }
       
        public int dt { get; set; }
        
        public int timezone { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public int cod { get; set; }

        public string IconUrl => $"https://openweathermap.org/img/wn/{weather[0].icon}@4x.png";
    }

    [JsonSerializable(typeof(List<WeatherData>))]
    internal sealed partial class WeatherDataContext : JsonSerializerContext { }
}
