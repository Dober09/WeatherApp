using System.Net.Http;
using System.Threading.Channels;
using System.Text.Json;
using WeatherApp.Models;



namespace WeatherApp.Services
{
    public class DataServices : IDataServices
    {
        private const string BaseUrl = "";
        public async Task<List<WeatherData>> LoadJsonDataAsync(string city)
        {
            using (HttpClient client = new HttpClient())
            {
                string url = $"{BaseUrl}?q={city}&appid={apikeys}&unit=metric";
                HttpResponseMessage response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<List<WeatherData>>(json);
                }
                else
                {
                    return null;
                }
            }
        
        }
    }
}
