using AntDesign.Charts;
using ClimateControl.Shared.Dtos;
using ClimateControl.WebClient.Resources;

namespace ClimateControl.WebClient.Helpers
{
    public static class AntChartHelper
    {
        private const float AccuracyUpperLimit = 100f;

        private static string GetXAxisDateTimeLabel(this BaseMonitoringDto monitoring)
        {
            return monitoring.TracedTime.HasValue is false ? string.Empty : monitoring.TracedTime.Value.ToString("HH:mm:ss");
        }

        public static IEnumerable<GraphicData> GetAccuracyData(List<MonitoringWithAccuracyDto> monitorings)
        {
            foreach (var monitoring in monitorings)
            {
                var time = monitoring.GetXAxisDateTimeLabel();

                if (monitoring.Accuracy is not null)
                {
                    yield return new GraphicData(time, monitoring.Accuracy.Humidity, "Влажность");
                    yield return new GraphicData(time, monitoring.Accuracy.Temperature, "Температура");
                }

                yield return new GraphicData(time, AccuracyUpperLimit, "Лимит");
            }
        }

        public static IEnumerable<GraphicData> GetTemperatureData<T>(List<T> monitorings, ConfigsDto config) where T : BaseMonitoringDto
        {
            var temperatureData = new List<GraphicData>();

            for (var i = 0; i < monitorings.Count; i++)
            {
                if (monitorings[i].TracedTime is null)
                {
                    if (TrySetDateTimeBasedOnNeighbors(monitorings, i, config) is false)
                    {
                        monitorings[i].TracedTime = DateTimeOffset.Now;
                    }
                }

                var graphicsData = GetGraphicDataForTemperatureMonitoring(monitorings[i], config);

                temperatureData.AddRange(graphicsData);
            }

            return temperatureData;
        }

        private static IEnumerable<GraphicData> GetGraphicDataForTemperatureMonitoring(BaseMonitoringDto monitoring, ConfigsDto config)
        {
            var graphicsData = new List<GraphicData>();

            var time = monitoring.GetXAxisDateTimeLabel();

            if (string.IsNullOrEmpty(time))
            {
                return Enumerable.Empty<GraphicData>();
            }

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

        public static IEnumerable<GraphicData> GetHumidityData<T>(List<T> monitorings, ConfigsDto config) where T : BaseMonitoringDto
        {
            try
            {
                var humidityData = new List<GraphicData>();

                for (var i = 0; i < monitorings.Count; i++)
                {
                    if (monitorings[i].TracedTime is null)
                    {
                        if (TrySetDateTimeBasedOnNeighbors(monitorings, i, config) is false)
                        {
                            monitorings[i].TracedTime = DateTimeOffset.Now;
                        }
                    }

                    var graphicsData = GetGraphicDataForHumidityMonitoring(monitorings[i], config);

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

        private static IEnumerable<GraphicData> GetGraphicDataForHumidityMonitoring(BaseMonitoringDto monitoring, ConfigsDto config)
        {
            var graphicsData = new List<GraphicData>();

            var time = monitoring.GetXAxisDateTimeLabel();

            if (string.IsNullOrEmpty(time))
            {
                return Enumerable.Empty<GraphicData>();
            }

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

        private static bool TrySetDateTimeBasedOnNeighbors<T>(IList<T> monitorings, int index, ConfigsDto monitoringConfig) where T : BaseMonitoringDto
        {
            if (monitorings.Any() is false || index < 0)
            {
                return false;
            }

            var firstMonWithTime = monitorings.First(mon => mon.TracedTime.HasValue);

            var elementWithTimeIndex = monitorings.IndexOf(firstMonWithTime);

            if (firstMonWithTime.TracedTime is null)
            {
                return false;
            }

            var firstNotNullTime = firstMonWithTime.TracedTime.Value;

            var resultTime = firstNotNullTime.AddSeconds((index - elementWithTimeIndex) * monitoringConfig.PredictionTimeIntervalSeconds);

            monitorings[index].TracedTime = resultTime;

            return true;
        }

        public static LineConfig GetAccuracyConfig(List<GraphicData> accuracyData)
        {
            var config = GetBaseLineConfig();

            config.Title.Text = "Точность прогноза температуры";
            config.YAxis.Title.Text = "Точнсть прогноза %";
            UpdateConfigsMinMaxLimits(ref config, accuracyData);
            config.Point.Shape = "square";

            return config;
        }

        public static LineConfig GetTemperatureLineConfig(List<GraphicData> temperatureData)
        {
            var config = GetBaseLineConfig();

            config.Title.Text = "Температура";
            config.YAxis.Title.Text = "Градусы, °C";
            UpdateConfigsMinMaxLimits(ref config, temperatureData);

            return config;
        }

        public static LineConfig GetHumidityLineConfig(List<GraphicData> humidityData)
        {
            var config = GetBaseLineConfig();

            config.Title.Text = "Относительная влажность";
            config.YAxis.Title.Text = "Процент влажности, %";
            UpdateConfigsMinMaxLimits(ref config, humidityData);
            config.Point.Shape = "circle";

            return config;
        }

        public static void UpdateConfigsMinMaxLimits(ref LineConfig plotConfig, List<GraphicData> data)
        {
            var min = data.Min(item => item.value);
            var max = data.Max(item => item.value);
            var delta = max - min;
            plotConfig.YAxis.Max = max + delta / 10;
            plotConfig.YAxis.Min = min - delta / 10;
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
                XField = $"{nameof(GraphicData.time)}",
                YField = $"{nameof(GraphicData.value)}",
                SeriesField = $"{nameof(GraphicData.type)}",
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
                                LineDash = new[] { 4, 2 }
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
