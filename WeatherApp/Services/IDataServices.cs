using WeatherApp.Models;

namespace WeatherApp.Services
{
    public interface IDataServices
    {
        Task<List<WeatherData>> LoadJsonDataAsync(String city);
    }
}
