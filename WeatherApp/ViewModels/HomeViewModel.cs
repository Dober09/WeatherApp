

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Diagnostics;
using WeatherApp.Models;
using WeatherApp.Services;
using WeatherApp.Views;

namespace WeatherApp.ViewModels
{
    public partial class HomeViewModel : BaseViewModel
    {
        public ObservableCollection<WeatherData> WeatherDataList { get; } = new();
        [ObservableProperty]
        bool isRefreshing;
        IConnectivity connectivity;
        IGeolocation geolocation;
        DataServices dataServices;

        public HomeViewModel(DataServices dataServices , IConnectivity connectivity, IGeolocation geolocation)
        {
            Title = "Current Weather";
            this.dataServices = dataServices;
            this.connectivity = connectivity;
            this.geolocation = geolocation;

            GetWeatherDataAsync();
        }



        [RelayCommand]
        async Task GoToDetailAsync(WeatherData weatherdata)
        {
            if (weatherdata is null) {
                return;
            }

            await Shell.Current.GoToAsync($"{nameof(DetailPage)}", new Dictionary<string, object> { { "WeatherData", weatherdata } });

        }

        [RelayCommand]
        async Task SearchPlaceAsync()
        {
            string message = await Shell.Current.DisplayPromptAsync("search", "", "ok", "cancel", "Enter City");
           var weatherData =  await dataServices.LoadJsonDataAsync(message);
            Debug.WriteLine(message);
            //Debug.WriteLine($"Weather data {weatherData}");
            if (WeatherDataList.Count != 0)
                WeatherDataList.Clear();



            foreach (var weather in weatherData)
                WeatherDataList.Add(weather);

        }


        private async Task GetWeatherDataAsync()
        {
            if (IsBusy)
                return;

            try
            {
                //Get Cached location , else get real location
                var location = await geolocation.GetLastKnownLocationAsync();

                if (location is not null)
                {
                    Debug.WriteLine("");
                    location = await geolocation.GetLocationAsync(new GeolocationRequest
                    {
                        DesiredAccuracy = GeolocationAccuracy.Medium,
                        Timeout = TimeSpan.FromSeconds(30)
                    });

                }
                if (connectivity.NetworkAccess != NetworkAccess.Internet)
                {
                    await Shell.Current.DisplayAlert("No connectivity!", "Please check internet and  try again.", "OK");
                    return;
                }

                IsBusy = true;

                Debug.WriteLine($"long => {location.Longitude} lati => {location.Latitude} ");

                var weatherData= await dataServices.LoadJsonDataAsync(location.Latitude, location.Longitude);
                //Debug.WriteLine($"Weather data {weatherData}");
                if (WeatherDataList.Count != 0)
                    WeatherDataList.Clear();



                foreach (var weather in weatherData)
                    WeatherDataList.Add(weather);

                Debug.WriteLine($" Weather data {WeatherDataList.Count}");

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to get weather: {ex.Message}");
                await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");

            }
            finally
            {
                IsBusy = false;
                IsRefreshing = false;
            }
        }
        
    }
}
