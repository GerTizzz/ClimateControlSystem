﻿using ClimateControlSystem.Server.Resources.Common;
using MediatR;

namespace ClimateControlSystem.Server.Services.Queries
{
    public sealed class PredictQuery : IRequest<Prediction>
    {
        public SensorsData SensorsData { get; }

        public PredictQuery(SensorsData sensorsData)
        {
            SensorsData = sensorsData;
        }
    }
}
