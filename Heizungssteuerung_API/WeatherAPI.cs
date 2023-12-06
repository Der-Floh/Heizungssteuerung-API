using Weather.NET;
using Weather.NET.Enums;
using Weather.NET.Models.WeatherModel;

namespace Heizungssteuerung_API;

public sealed class WeatherAPI
{
    public string APIKey { get => _apiKey; set { _apiKey = value; _client = new WeatherClient(value); } }
    private string _apiKey = "6b50f64b6769a340c588baf0eb0e2417";
    public string City { get; set; } = "Krefeld";
    private WeatherClient _client;

    public WeatherAPI(string? apiKey = null)
    {
        if (!string.IsNullOrEmpty(apiKey))
            APIKey = apiKey;

        _client = new WeatherClient(APIKey);
    }

    public async Task<double> GetCurrentTemperature()
    {
        WeatherModel weather = await _client.GetCurrentWeatherAsync(City, Measurement.Metric);
        return weather.Main.Temperature;
    }

    public async Task<Weather> GetCurrentWeather()
    {
        WeatherModel weatherModel = await _client.GetCurrentWeatherAsync(City, Measurement.Metric);
        Weather weather = new Weather(weatherModel);
        return weather;
    }

    public async Task<double[]> GetFutureTemperatures(int forecastCount)
    {
        List<WeatherModel> weatherData = await _client.GetForecastAsync(City, forecastCount, Measurement.Metric);
        double[] temperatures = new double[weatherData.Count];
        for (int i = 0; i < weatherData.Count; i++)
            temperatures[i] = weatherData[i].Main.Temperature;
        return temperatures;
    }

    public async Task<double[]> GetFutureDaysTemperatures(int dayCount)
    {
        int timeStampCount = 24 * dayCount / 3;
        return await GetFutureTemperatures(timeStampCount);
    }

    public async Task<Weather[]> GetFutureWeather(int forecastCount)
    {
        List<WeatherModel> weatherData = await _client.GetForecastAsync(City, forecastCount, Measurement.Metric);
        Weather[] temperatures = new Weather[weatherData.Count];
        for (int i = 0; i < weatherData.Count; i++)
            temperatures[i] = new Weather(weatherData[i]);
        return temperatures;
    }

    public async Task<Weather[]> GetFutureDaysWeather(int dayCount)
    {
        int timeStampCount = 24 * dayCount / 3;
        return await GetFutureWeather(timeStampCount);
    }
}
