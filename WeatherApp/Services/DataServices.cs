using System.Net.Http;
using System.Threading.Channels;
using System.Text.Json;
using WeatherApp.Models;
using System.Net.Http.Json;
using System.Diagnostics;



namespace WeatherApp.Services
{
    public class DataServices : IDataServices
    {
        private const string BaseUrl = "https://api.openweathermap.org/data/2.5/weather";
        private const string apiKey = "a1af0223e4575c2294f7e6b4705052c3";
        HttpClient httpClient;
        List<WeatherData> weatherDataList;
        public DataServices() {
            this.httpClient = new HttpClient();
        }
        public async Task<List<WeatherData>> LoadJsonDataAsync(double lat,double lon)
        {
           string url = $"{BaseUrl}?lat={lat}&lon={lon}&appid={apiKey}";

            if (weatherDataList?.Count > 0) return weatherDataList;

            //online
            var response = await httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            { // Create a list with single weather data since API returns single object
                var weatherData = await response.Content.ReadFromJsonAsync<WeatherData>();
                weatherDataList = new List<WeatherData> { weatherData };

            }
                return weatherDataList;
            
           


            // Offline
            //using var stream = await FileSystem.OpenAppPackageFileAsync("data.json");
            //using var reader = new StreamReader(stream);
            //var contents = await reader.ReadToEndAsync();
            //weatherdataList = JsonSerializer.Deserialize(contents, WeatherDataContext.Default.ListWeatherData);
            //}



        }
    }
}
