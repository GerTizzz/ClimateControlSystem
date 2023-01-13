using AntDesign.Charts;
using ClimateControlSystem.Client.Resources;
using ClimateControlSystem.Shared;
using ClimateControlSystem.Shared.Common;

namespace ClimateControlSystem.Client.Shared
{
    public static class AntChartHelper
    {
        public static List<GraphicData> GetAccuracyData(List<MonitoringResponse> _predictions)
        {
            List<GraphicData> temperatureAccuracy = new List<GraphicData>();

            for (int i = 0; i < _predictions.Count; i++)
            {
                if (_predictions[i].PredictedHumidityAccuracy.HasValue is false || _predictions[i].PredictedTemperatureAccuracy.HasValue is false)
                {
                    continue;
                }

                temperatureAccuracy.Add(new GraphicData(_predictions[i].MeasurementTime.ToString("HH:mm:ss dd:MM:yyyy"),
                    _predictions[i].PredictedTemperatureAccuracy.Value, "Temperature"));

                temperatureAccuracy.Add(new GraphicData(_predictions[i].MeasurementTime.ToString("HH:mm:ss dd:MM:yyyy"),
                    _predictions[i].PredictedHumidityAccuracy.Value, "Humidity"));

                temperatureAccuracy.Add(new GraphicData(_predictions[i].MeasurementTime.ToString("HH:mm:ss dd:MM:yyyy"),
                    100f, "Limit"));
            }

            return temperatureAccuracy;
        }

        public static List<GraphicData> GetTemperatureData(List<MonitoringResponse> predictions, Config config)
        {
            List<GraphicData> temperatureData = new List<GraphicData>();

            for (int i = 0; i < predictions.Count; i++)
            {
                temperatureData.Add(new GraphicData(predictions[i].MeasurementTime.ToString("HH:mm:ss dd:MM:yyyy"),
                    predictions[i].PredictedFutureTemperature, "Спрогнозированная"));
                temperatureData.Add(new GraphicData(predictions[i].MeasurementTime.ToString("HH:mm:ss dd:MM:yyyy"),
                    config.UpperTemperatureWarningLimit, "Верхний лимит"));        
                temperatureData.Add(new GraphicData(predictions[i].MeasurementTime.ToString("HH:mm:ss dd:MM:yyyy"),
                    config.LowerTemperatureWarningLimit, "Нижний лимит"));  

                if (i > 0)
                {
                    temperatureData.Add(new GraphicData(predictions[i - 1].MeasurementTime.ToString("HH:mm:ss dd:MM:yyyy"),
                        predictions[i].CurrentRealTemperature.Value, "Действительная"));
                }
            }

            return temperatureData;
        }

        public static List<GraphicData> GetHumidityData(List<MonitoringResponse> predictions, Config config)
        {
            List<GraphicData> humidityData = new List<GraphicData>();

            for (int i = 0; i < predictions.Count; i++)
            {
                humidityData.Add(new GraphicData(predictions[i].MeasurementTime.ToString("HH:mm:ss dd:MM:yyyy"),
                    predictions[i].PredictedFutureHumidity, "Спрогнозированная"));
                humidityData.Add(new GraphicData(predictions[i].MeasurementTime.ToString("HH:mm:ss dd:MM:yyyy"),
                    config.UpperHumidityWarningLimit, "Верхний лимит"));
                humidityData.Add(new GraphicData(predictions[i].MeasurementTime.ToString("HH:mm:ss dd:MM:yyyy"),
                    config.LowerHumidityWarningLimit, "Нижний лимит"));

                if (i > 0)
                {
                    humidityData.Add(new GraphicData(predictions[i - 1].MeasurementTime.ToString("HH:mm:ss dd:MM:yyyy"),
                        predictions[i].CurrentRealHumidity.Value, "Действительная"));
                }
            }

            return humidityData;
        }

        public static LineConfig GetAccuracyConfig(List<GraphicData> accuracy)
        {
            var config = GetBaseLineConfig();

            config.Title.Text = "Точность прогноза температуры";
            config.YAxis.Title.Text = "Точнсть прогноза %";
            var min = accuracy.Min(item => item.value);
            var max = accuracy.Max(item => item.value);
            var delta = max - min;
            config.YAxis.Max = max + delta / 10;
            config.YAxis.Min = min - delta / 10;
            config.Point.Shape = "square";

            return config;
        }

        public static LineConfig GetTemperatureLineConfig(List<GraphicData> actualAndPredictedTemperature)
        {
            var config = GetBaseLineConfig();

            config.Title.Text = "Температура";
            config.YAxis.Title.Text = "Градусы, °C";
            var min = actualAndPredictedTemperature.Min(item => item.value);
            var max = actualAndPredictedTemperature.Max(item => item.value);
            var delta = max - min;
            config.YAxis.Max = max + delta / 10;
            config.YAxis.Min = min - delta / 10;

            return config;
        }

        public static LineConfig GetHumidityLineConfig(List<GraphicData> actualAndPredictedHumidity)
        {
            var config = GetBaseLineConfig();

            config.Title.Text = "Относительная влажность";
            config.YAxis.Title.Text = "Процент влажности, %";
            var min = actualAndPredictedHumidity.Min(item => item.value);
            var max = actualAndPredictedHumidity.Max(item => item.value);
            var delta = max - min;
            config.YAxis.Max = max + delta / 10;
            config.YAxis.Min = min - delta / 10;
            config.Point.Shape = "circle";

            return config;
        }

        private static LineConfig GetBaseLineConfig()
        {
            return new LineConfig()
            {
                Title = new Title()
                {
                    Visible = true,
                    Style = new TextStyle()
                    {
                        Fill = "#000",
                        FontSize = 20
                    }
                },
                Padding = "auto",
                ForceFit = true,
                XField = "time",
                YField = "value",
                SeriesField = "type",
                Point = new LineViewConfigPoint()
                {
                    Style = new GraphicStyle() 
                    { 
                        LineWidth = 3,
                        FillOpacity = 5
                    },
                    Size = 7,
                    Visible = true,
                    Shape = "diamond"
                },
                LineStyle = new LineStyle()
                {
                    LineWidth = 5
                },
                Legend = new Legend()
                {
                    Position = "top-right",
                    Text = new LegendText()
                    {
                        Style = new TextStyle()
                        {
                            Fill = "#000",
                            FontSize = 20
                        }
                    }
                },
                XAxis = new ValueCatTimeAxis()
                {
                    Title = new BaseAxisTitle()
                    {
                        Text = "Время",
                        Style = new TextStyle()
                        {
                            FontSize = 16
                        },
                        Visible = true
                    },
                    Visible = true,
                    TickLine = new BaseAxisTickLine()
                    {
                        Visible = true,
                        Style = new LineStyle()
                        {
                            LineWidth = 2,
                            Stroke = "#aaa"
                        },
                    },
                    Line = new BaseAxisLine()
                    {
                        Visible = true,
                        Style = new LineStyle()
                        {
                            Stroke = "#aaa"
                        }
                    },
                    Grid = new BaseAxisGrid()
                    {
                        Visible = true,
                        Line = new BaseAxisGridLine()
                        {
                            Type = "",
                            Style = new LineStyle()
                            {
                                Stroke = "#ddd",
                                LineDash = new int[] { 4, 2 }
                            }
                        }
                    }
                },
                YAxis = new ValueAxis()
                {
                    Title = new BaseAxisTitle()
                    {
                        Style = new TextStyle()
                        {
                            FontSize = 16,
                        },
                        AutoRotate = true,
                        Visible = true
                    },
                    Visible = true
                },
            };
        }
    }
}
