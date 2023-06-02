using Shared.Dtos;
using WebClient.Resources;

namespace WebClient.Helpers;

public static class AntDataHelper
{
    public static IEnumerable<GraphicData> GetHistoryData(ForecastDto? forecast, ConfigsDto config)
    {
        if (forecast is null || forecast.Predictions is null || forecast.Predictions.Any() is false)
        {
            return Enumerable.Empty<GraphicData>();
        }

        return GetHistoryGraphicData(forecast, config);
    }

    private static IEnumerable<GraphicData> GetHistoryGraphicData(ForecastDto forecast, ConfigsDto config)
    {
        var graphicsData = new List<GraphicData>();

        var startTime = forecast.Time;

        for (int i = 0; i < forecast.Predictions.Count; i++)
        {
            var time = startTime.AddMinutes(10 * (i + 1)).ToString("HH:mm:ss");

            graphicsData.Add(new GraphicData(time,
                forecast.Predictions[i].Value, "Архив"));

            graphicsData.Add(new GraphicData(time,
                config.UpperTemperatureWarningLimit, "Верхний лимит"));
            graphicsData.Add(new GraphicData(time,
                config.LowerTemperatureWarningLimit, "Нижний лимит"));
        }

        return graphicsData;
    }

    public static IEnumerable<GraphicData> GetPredictedData(ForecastDto? forecast, ConfigsDto config)
    {
        if (forecast is null || forecast.Predictions is null || forecast.Predictions.Any() is false)
        {
            return Enumerable.Empty<GraphicData>();
        }

        return GetPredictionGraphicData(forecast, config);
    }

    private static IEnumerable<GraphicData> GetPredictionGraphicData(ForecastDto forecast, ConfigsDto config)
    {
        var graphicsData = new List<GraphicData>();

        var startTime = forecast.Time;

        for (int i = 0; i < forecast.Predictions.Count; i++)
        {
            var time = startTime.AddMinutes(10 * (i + 1)).ToString("HH:mm:ss");

            graphicsData.Add(new GraphicData(time,
                forecast.Predictions[i].Value, "Прогноз"));

            graphicsData.Add(new GraphicData(time,
                config.UpperTemperatureWarningLimit, "Верхний лимит"));
            graphicsData.Add(new GraphicData(time,
                config.LowerTemperatureWarningLimit, "Нижний лимит"));
        }

        return graphicsData;
    }
}