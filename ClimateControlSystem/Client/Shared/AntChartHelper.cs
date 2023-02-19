using AntDesign.Charts;
using ClimateControlSystem.Client.Resources;
using ClimateControlSystem.Shared.Common;
using ClimateControlSystem.Shared.SendToClient;

namespace ClimateControlSystem.Client.Shared
{
    public static class AntChartHelper
    {
        private const float AccuracyUpperLimit = 100f;
        private const int MaxGraphicsDataPerMonitoringResponse = 4;

        private static List<GraphicData> GetNewTemperatureGraphicData(BaseMonitoringDTO monitoring, ConfigResponse config)
        {
            try
            {
                List<GraphicData> graphicsData = new List<GraphicData>(MaxGraphicsDataPerMonitoringResponse);

                string time = monitoring.TracedTime.Value.ToString("HH:mm:ss dd.MM.yyyy");

                if (monitoring.Prediction is not null)
                {
                    graphicsData.Add(new GraphicData(time,
                        monitoring.Prediction.Temperature, "Спрогнозированная"));
                }

                graphicsData.Add(new GraphicData(time,
                    config.UpperTemperatureWarningLimit, "Верхний лимит"));
                graphicsData.Add(new GraphicData(time,
                    config.LowerTemperatureWarningLimit, "Нижний лимит"));

                if (monitoring.ActualData is not null)
                {
                    graphicsData.Add(new GraphicData(time,
                        monitoring.ActualData.Temperature, "Действительная"));
                }

                return graphicsData;
            }
            catch
            {
                throw;
            }
        }

        private static List<GraphicData> GetNewHumidityGraphicsData(BaseMonitoringDTO monitoring, ConfigResponse config)
        {
            try
            {
                List<GraphicData> graphicsData = new List<GraphicData>(MaxGraphicsDataPerMonitoringResponse);

                string time = monitoring.TracedTime.Value.ToString("HH:mm:ss dd.MM.yyyy");

                if (monitoring.Prediction is not null)
                {
                    graphicsData.Add(new GraphicData(time,
                        monitoring.Prediction.Humidity, "Спрогнозированная"));
                }

                graphicsData.Add(new GraphicData(time,
                    config.UpperHumidityWarningLimit, "Верхний лимит"));
                graphicsData.Add(new GraphicData(time,
                    config.LowerHumidityWarningLimit, "Нижний лимит"));

                if (monitoring.ActualData is not null)
                {
                    graphicsData.Add(new GraphicData(time,
                        monitoring.ActualData.Humidity, "Действительная"));
                }

                return graphicsData;
            }
            catch
            {
                throw;
            }
        }

        public static List<GraphicData> GetAccuracyData(List<MonitoringWithAccuraciesDTO> monitorings)
        {
            try
            {
                List<GraphicData> temperatureAccuracy = new List<GraphicData>();

                for (int i = 0; i < monitorings.Count; i++)
                {
                    string time = monitorings[i].TracedTime.Value.ToString("HH:mm:ss dd.MM.yyyy");

                    if (monitorings[i].Accuracy is not null)
                    {
                        temperatureAccuracy.Add(new GraphicData(time, monitorings[i].Accuracy.Humidity, "Влажность"));
                        temperatureAccuracy.Add(new GraphicData(time, monitorings[i].Accuracy.Temperature, "Температура"));
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

        public static List<GraphicData> GetTemperatureData<T>(List<T> monitorings, ConfigResponse config) where T : BaseMonitoringDTO
        {
            try
            {
                int maxHumidityDataSize = monitorings.Count * MaxGraphicsDataPerMonitoringResponse;

                List<GraphicData> temperatureData = new List<GraphicData>(maxHumidityDataSize);

                for (int i = 0; i < monitorings.Count; i++)
                {
                    if (monitorings[i].TracedTime is null)
                    {
                        if (TrySetDateTimeBasedOnNeighbors(monitorings, i, config) is false)
                        {
                            monitorings[i].TracedTime = DateTimeOffset.Now;
                        }
                    }

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

        private static bool TrySetDateTimeBasedOnNeighbors<T>(List<T> monitorings, int index, ConfigResponse config) where T : BaseMonitoringDTO
        {
            var firstMonWithTime = monitorings.FirstOrDefault(mon => mon.TracedTime.HasValue);
            
            if (firstMonWithTime is not null)
            {
                int elementWithTimeIndex = monitorings.IndexOf(firstMonWithTime);

                var resultTime = firstMonWithTime.TracedTime.Value.AddSeconds((index - elementWithTimeIndex) * config.PredictionTimeIntervalSeconds);

                monitorings[index].TracedTime = resultTime;

                return true;
            }
            else
            {
                return false;
            }
        }

        public static List<GraphicData> GetHumidityData<T>(List<T> monitorings, ConfigResponse config) where T : BaseMonitoringDTO
        {
            try
            {
                int maxHumidityDataSize = monitorings.Count * MaxGraphicsDataPerMonitoringResponse;

                List<GraphicData> humidityData = new List<GraphicData>(maxHumidityDataSize);

                for (int i = 0; i < monitorings.Count; i++)
                {
                    if (monitorings[i].TracedTime is null)
                    {
                        if (TrySetDateTimeBasedOnNeighbors(monitorings, i, config) is false)
                        {
                            monitorings[i].TracedTime = DateTimeOffset.Now;
                        }
                    }

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
