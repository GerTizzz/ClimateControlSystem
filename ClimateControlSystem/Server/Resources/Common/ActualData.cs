﻿namespace ClimateControlSystem.Server.Resources.Common
{
    public sealed class ActualData
    {
        public float Temperature { get; set; }
        public float Humidity { get; set; }

        public ActualData Clone()
        {
            var clone = new ActualData()
            {
                Temperature = Temperature,
                Humidity = Humidity
            };

            return clone;
        }
    }
}
