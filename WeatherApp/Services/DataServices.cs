using System.Net.Http;
using System.Threading.Channels;
using System.Text.Json;
using WeatherApp.Models;
using System.Net.Http.Json;



namespace WeatherApp.Services
{
    public class DataServices : IDataServices
    {
        private const string BaseUrl = "https://api.openweathermap.org/data/2.5/weather";
        private const string apiKey = "";
        HttpClient httpClient;
        List<WeatherData> weatherdataList;
        public DataServices() {
            this.httpClient = new HttpClient();
        }
        public async Task<List<WeatherData>> LoadJsonDataAsync(string city)
        {
           string url = $"{BaseUrl}?q={city}&appid={apiKey}";

            if (weatherdataList?.Count > 0) return weatherdataList;

            //online
            var response = await httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                weatherdataList = await response.Content.ReadFromJsonAsync(WeatherDataContext.Default.ListWeatherData);
            }
            else
            {

               
            // Offline
            using var stream = await FileSystem.OpenAppPackageFileAsync("data.json");
            using var reader = new StreamReader(stream);
            var contents = await reader.ReadToEndAsync();
            weatherdataList = JsonSerializer.Deserialize(contents, WeatherDataContext.Default.ListWeatherData);
            }


            return weatherdataList;

        }
    }
}
