using AutoMapper;
using ClimateControlSystem.Server.Domain.Services;
using ClimateControlSystem.Server.Resources.Common;
using ClimateControlSystem.Server.Services.MediatR.Queries;
using ClimateControlSystem.Server.Services.PredictionEngine.PredictionEngineResources;

namespace ClimateControlSystem.Server.Services.MediatR.Handlers
{
    public sealed class GetPredictionHandler
    {
        private readonly IPredictionEngineService _predictionEngine;
        private readonly IMapper _mapper;

        public GetPredictionHandler(IPredictionEngineService predictionEngine, IMapper mapper)
        {
            _predictionEngine = predictionEngine;
            _mapper = mapper;
        }

        public async Task<Prediction> Handle(GetPredictionQuery request, CancellationToken cancellationToken)
        {
            var tensorPredictionRequest = _mapper.Map<TensorPredictionRequest>(request.sensorsData);

            var tensorPredictionResult = await _predictionEngine.Predict(tensorPredictionRequest);

            var prediction = _mapper.Map<Prediction>(tensorPredictionResult);

            return prediction;
        }
    }
}
