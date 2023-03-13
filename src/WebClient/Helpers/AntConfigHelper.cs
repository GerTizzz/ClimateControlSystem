using AntDesign.Charts;
using WebClient.Resources;

namespace WebClient.Helpers
{
    public static class AntConfigHelper
    {
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
