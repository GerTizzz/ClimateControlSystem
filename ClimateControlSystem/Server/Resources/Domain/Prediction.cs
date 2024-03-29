﻿namespace ClimateControlSystem.Server.Resources.Common
{
    public class Prediction
    {
        public float Temperature { get; set; }
        public float Humidity { get; set; }
        public FeaturesData Features { get; set; }

        public Prediction Clone()
        {
            var clone = new Prediction()
            {
                Temperature = Temperature,
                Humidity = Humidity,
                Features = Features
            };

            return clone;
        }
    }
}
