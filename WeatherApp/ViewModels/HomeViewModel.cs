

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using WeatherApp.Models;
using WeatherApp.Services;

namespace WeatherApp.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        public ObservableCollection<WeatherData> WeatherDataList { get; } = new();
        [ObservableProperty]
        bool isRefreshing;
        IConnectivity connectivity;
        DataServices dataServices;

        public HomeViewModel(DataServices dataServices ,IConnectivity connectivity)
        {
            Title = "Current Weather";
            this.dataServices = dataServices;
            this.connectivity = connectivity;

        }

        [RelayCommand] 
        async Task GoToDetail(WeatherData weatherData)
        {

        }



        [RelayCommand]
        async Task GetWeatherDataAsync()
        {

        }
        
    }
}
