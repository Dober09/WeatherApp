using CommunityToolkit.Mvvm.ComponentModel;
using WeatherApp.Models;




namespace WeatherApp.ViewModels
{

    [QueryProperty(nameof(WeatherData),"WeatherData")]
    public partial class DetailViewModel: BaseViewModel
    {
        [ObservableProperty]
        WeatherData weatherData;


    }
}
