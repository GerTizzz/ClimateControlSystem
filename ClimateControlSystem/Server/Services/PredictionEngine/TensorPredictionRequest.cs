﻿using Microsoft.ML.Data;

namespace ClimateControlSystem.Server.Services.PredictionEngine
{
    internal sealed class TensorPredictionRequest
    {
        [VectorType(12)]
        public float[] serving_default_input_1 { get; set; }
    }
}
