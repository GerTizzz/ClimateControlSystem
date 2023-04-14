using Shared.Dtos;
using WebClient.Resources;

namespace WebClient.Helpers;

public static class AntDataHelper
{
    private const float AccuracyUpperLimit = 0f;

    private static string GetXAxisDateTimeLabel(this ForecastDto monitoring)
    {
        return monitoring.Time.HasValue is false ? string.Empty : monitoring.Time.Value.ToString("HH:mm:ss");
    }

    public static IEnumerable<GraphicData> GetError(List<ForecastDto> monitorings)
    {
        foreach (var monitoring in monitorings)
        {
            var time = monitoring.GetXAxisDateTimeLabel();

            if (monitoring.Error is not null)
            {
                yield return new GraphicData(time, monitoring.Error.Humidity, "Влажность");
                yield return new GraphicData(time, monitoring.Error.Temperature, "Температура");
            }

            yield return new GraphicData(time, AccuracyUpperLimit, "Лимит");
        }
    }

    public static IEnumerable<GraphicData> GetTemperature(IEnumerable<ForecastDto> forecasts, ConfigsDto config)
    {
        var temperatureData = new List<GraphicData>();

        foreach (var graphicsData in forecasts.Select(forecast => GetTemperatureGraphicData(forecast, config)))
        {
            temperatureData.AddRange(graphicsData);
        }

        return temperatureData;
    }

    public static IEnumerable<GraphicData> GetHumidity(List<ForecastDto> forecasts, ConfigsDto config)
    {
        try
        {
            var humidityData = new List<GraphicData>();

            foreach (var graphicsData in forecasts.Select(forecast => GetHumidityGraphicData(forecast, config)))
            {
                humidityData.AddRange(graphicsData);
            }

            return humidityData;
        }
        catch (Exception exc)
        {
            Console.Write(exc.Message);

            return new List<GraphicData>();
        }
    }

    private static IEnumerable<GraphicData> GetTemperatureGraphicData(ForecastDto monitoring, ConfigsDto config)
    {
        var graphicsData = new List<GraphicData>();

        var time = monitoring.GetXAxisDateTimeLabel();

        if (string.IsNullOrEmpty(time))
        {
            return Enumerable.Empty<GraphicData>();
        }

        if (monitoring.Label is not null)
        {
            graphicsData.Add(new GraphicData(time,
                monitoring.Label.Temperature, "Спрогнозированная"));
        }

        graphicsData.Add(new GraphicData(time,
            config.UpperTemperatureWarningLimit, "Верхний лимит"));
        graphicsData.Add(new GraphicData(time,
            config.LowerTemperatureWarningLimit, "Нижний лимит"));

        if (monitoring.Fact is not null)
        {
            graphicsData.Add(new GraphicData(time,
                monitoring.Fact.Temperature, "Действительная"));
        }

        return graphicsData;
    }

    private static IEnumerable<GraphicData> GetHumidityGraphicData(ForecastDto monitoring, ConfigsDto config)
    {
        var graphicsData = new List<GraphicData>();

        var time = monitoring.GetXAxisDateTimeLabel();

        if (string.IsNullOrEmpty(time))
        {
            return Enumerable.Empty<GraphicData>();
        }

        if (monitoring.Label is not null)
        {
            graphicsData.Add(new GraphicData(time,
                monitoring.Label.Humidity, "Спрогнозированная"));
        }

        graphicsData.Add(new GraphicData(time,
            config.UpperHumidityWarningLimit, "Верхний лимит"));
        graphicsData.Add(new GraphicData(time,
            config.LowerHumidityWarningLimit, "Нижний лимит"));

        if (monitoring.Fact is not null)
        {
            graphicsData.Add(new GraphicData(time,
                monitoring.Fact.Humidity, "Действительная"));
        }

        return graphicsData;
    }        
}