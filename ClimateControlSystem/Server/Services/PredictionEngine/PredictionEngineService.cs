﻿using AutoMapper;
using ClimateControlSystem.Server.Domain.Services;
using ClimateControlSystem.Server.Resources;
using Microsoft.ML;
using Microsoft.ML.Transforms;

namespace ClimateControlSystem.Server.Services.PredictionEngine
{
    public sealed class PredictionEngineService : IPredictionEngineService
    {
        private readonly IMapper _mapper;

        private readonly PredictionEngine<TensorPredictionRequest, TensorPredictionResult> _predictionEgine;

        public PredictionEngineService(IMapper mapper, string modelLocation)
        {
            _mapper = mapper;
            _predictionEgine = CreatePredictionEgine(modelLocation);
        }

        public PredictionResult Predict(PredictionRequest incomingRequest)
        {
            TensorPredictionRequest features = _mapper.Map<TensorPredictionRequest>(incomingRequest);

            TensorPredictionResult prediction = _predictionEgine.Predict(features);

            PredictionResult predictionResult = _mapper.Map<PredictionResult>(prediction);

            return predictionResult;
        }

        private PredictionEngine<TensorPredictionRequest, TensorPredictionResult> CreatePredictionEgine(string modelLocation)
        {
            MLContext mlContext = new MLContext();

            TensorFlowModel model = mlContext.Model.LoadTensorFlowModel(modelLocation);

            TensorFlowEstimator pipeline = model.ScoreTensorFlowModel(
                new[] { "StatefulPartitionedCall" },
                new[] { "serving_default_input_1" });

            TensorFlowTransformer transformer = pipeline.Fit(CreateEmptyDataView(mlContext));

            return mlContext.Model.CreatePredictionEngine<TensorPredictionRequest, TensorPredictionResult>(transformer);
        }

        private IDataView CreateEmptyDataView(MLContext mlContext)
        {
            IEnumerable<TensorPredictionRequest> enumerableData = new List<TensorPredictionRequest>();
            return mlContext.Data.LoadFromEnumerable(enumerableData);
        }
    }
}
