using AntDesign.Charts;
using ClimateControlSystem.Client.Resources;
using ClimateControlSystem.Shared.Common;
using ClimateControlSystem.Shared.SendToClient;

namespace ClimateControlSystem.Client.Shared
{
    public static class AntChartHelper
    {
        private const float AccuracyUpperLimit = 100f;
        private const int MaxGraphicsDataElementsPerMonitoringResponse = 4;

        public static List<GraphicData> GetNewTemperatureGraphicData(PredictionResponse newResonse, ConfigResponse config)
        {
            try
            {
                List<GraphicData> graphicsData = new List<GraphicData>(MaxGraphicsDataElementsPerMonitoringResponse);

                string time = newResonse.MeasurementTime.ToString("HH:mm:ss dd.MM.yyyy");

                graphicsData.Add(new GraphicData(time,
                    newResonse.PredictedFutureTemperature, "Спрогнозированная"));
                graphicsData.Add(new GraphicData(time,
                    config.UpperTemperatureWarningLimit, "Верхний лимит"));
                graphicsData.Add(new GraphicData(time,
                    config.LowerTemperatureWarningLimit, "Нижний лимит"));

                if (newResonse.CurrentRealTemperature.HasValue)
                {
                    graphicsData.Add(new GraphicData(time,
                        newResonse.CurrentRealTemperature.Value, "Действительная"));
                }

                return graphicsData;
            }
            catch
            {
                throw;
            }
        }

        public static List<GraphicData> GetNewHumidityGraphicsData(PredictionResponse newResonse, ConfigResponse config)
        {
            try
            {
                List<GraphicData> graphicsData = new List<GraphicData>(MaxGraphicsDataElementsPerMonitoringResponse);

                string time = newResonse.MeasurementTime.ToString("HH:mm:ss dd.MM.yyyy");

                graphicsData.Add(new GraphicData(time,
                    newResonse.PredictedFutureHumidity, "Спрогнозированная"));
                graphicsData.Add(new GraphicData(time,
                    config.UpperHumidityWarningLimit, "Верхний лимит"));
                graphicsData.Add(new GraphicData(time,
                    config.LowerHumidityWarningLimit, "Нижний лимит"));

                if (newResonse.CurrentRealHumidity.HasValue)
                {
                    graphicsData.Add(new GraphicData(time,
                        newResonse.CurrentRealHumidity.Value, "Действительная"));
                }

                return graphicsData;
            }
            catch
            {
                throw;
            }
        }

        public static List<GraphicData> GetAccuracyData(List<PredictionResponse> monitorings)
        {
            try
            {
                List<GraphicData> temperatureAccuracy = new List<GraphicData>();

                for (int i = 0; i < monitorings.Count; i++)
                {
                    string time = monitorings[i].MeasurementTime.Value.ToString("HH:mm:ss dd.MM.yyyy");

                    if (monitorings[i].PredictedHumidityAccuracy.HasValue)
                    {
                        temperatureAccuracy.Add(new GraphicData(time, monitorings[i].PredictedHumidityAccuracy.Value, "Влажность"));
                    }             
                    if (monitorings[i].PredictedTemperatureAccuracy.HasValue)
                    {
                        temperatureAccuracy.Add(new GraphicData(time, monitorings[i].PredictedTemperatureAccuracy.Value, "Температура"));
                    }

                    temperatureAccuracy.Add(new GraphicData(time, AccuracyUpperLimit, "Лимит"));
                }

                return temperatureAccuracy;
            }
            catch (Exception exc)
            {
                Console.Write(exc.Message);
                return new List<GraphicData>();
            }
        }

        public static List<GraphicData> GetTemperatureData(List<PredictionResponse> monitorings, ConfigResponse config)
        {
            try
            {
                int maxHumidityDataSize = monitorings.Count * MaxGraphicsDataElementsPerMonitoringResponse;

                List<GraphicData> temperatureData = new List<GraphicData>(maxHumidityDataSize);

                for (int i = 0; i < monitorings.Count; i++)
                {
                    var graphicsData = GetNewTemperatureGraphicData(monitorings[i], config);

                    temperatureData.AddRange(graphicsData);
                }

                return temperatureData;
            }
            catch (Exception exc)
            {
                Console.Write(exc.Message);
                return new List<GraphicData>();
            }
        }

        public static List<GraphicData> GetHumidityData(List<PredictionResponse> monitorings, ConfigResponse config)
        {
            try
            {
                int maxHumidityDataSize = monitorings.Count * MaxGraphicsDataElementsPerMonitoringResponse;

                List<GraphicData> humidityData = new List<GraphicData>(maxHumidityDataSize);

                for (int i = 0; i < monitorings.Count; i++)
                {
                    var graphicsData = GetNewHumidityGraphicsData(monitorings[i], config);

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
