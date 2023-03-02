﻿namespace ClimateControlSystem.Server.Resources.Domain
{
    public sealed class MicroclimatesEvents
    {
        public float? Temperature { get; set; }
        public float? Humidity { get; set; }

        public MicroclimatesEvents Clone()
        {
            var clone = new MicroclimatesEvents()
            {
                Temperature = Temperature,
                Humidity = Humidity
            };

            return clone;
        }
    }
}
