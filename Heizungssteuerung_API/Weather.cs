using Weather.NET.Models.WeatherModel;

namespace Heizungssteuerung_API;

public sealed class Weather
{
    public bool Rainy { get => RainVolume > 0; }
    public double RainVolume { get; set; }
    public bool Snowy { get => SnowVolume > 0; }
    public double SnowVolume { get; set; }
    public bool Cloudy { get => Cloudiness > 0; }
    public double Cloudiness { get; set; }
    public double Temperature { get; set; }

    public Weather(WeatherModel? weatherModel = null)
    {
        if (weatherModel is not null)
        {
            RainVolume = weatherModel.Rain?.Past3HoursVolume ?? 0;
            SnowVolume = weatherModel.Snow?.Past3HoursVolume ?? 0;
            Cloudiness = weatherModel.Clouds.Percentage / 100;
            Temperature = weatherModel.Main.Temperature;
        }
    }
}
