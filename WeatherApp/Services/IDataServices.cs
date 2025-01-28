using WeatherApp.Models;

namespace WeatherApp.Services
{
    public interface IDataServices
    {
        Task<List<WeatherData>> LoadJsonDataAsync(double lon , double lat);
        Task<List<WeatherData>> LoadJsonDataAsync(string city);
    }
}
