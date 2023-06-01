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

    public static IEnumerable<GraphicData> GetTemperature(IEnumerable<ForecastDto> forecasts, ConfigsDto config)
    {
        var temperatureData = new List<GraphicData>();

        foreach (var graphicsData in forecasts.Select(forecast => GetTemperatureGraphicData(forecast, config)))
        {
            temperatureData.AddRange(graphicsData);
        }

        return temperatureData;
    }

    private static IEnumerable<GraphicData> GetTemperatureGraphicData(ForecastDto monitoring, ConfigsDto config)
    {
        var graphicsData = new List<GraphicData>();

        var time = monitoring.GetXAxisDateTimeLabel();

        if (string.IsNullOrEmpty(time))
        {
            return Enumerable.Empty<GraphicData>();
        }

        foreach (var prediction in monitoring.Predictions)
        {
            graphicsData.Add(new GraphicData(time,
                prediction.Value, "Спрогнозированная"));

            graphicsData.Add(new GraphicData(time,
                config.UpperTemperatureWarningLimit, "Верхний лимит"));
            graphicsData.Add(new GraphicData(time,
                config.LowerTemperatureWarningLimit, "Нижний лимит"));
        }

        return graphicsData;
    }
}