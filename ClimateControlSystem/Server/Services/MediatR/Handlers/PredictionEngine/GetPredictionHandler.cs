using AutoMapper;
using ClimateControlSystem.Server.Domain.Services;
using ClimateControlSystem.Server.Resources.Common;
using ClimateControlSystem.Server.Services.MediatR.Queries;
using ClimateControlSystem.Server.Services.MediatR.Queries.PredictionEngine;
using ClimateControlSystem.Server.Services.PredictionEngine.PredictionEngineResources;
using MediatR;

namespace ClimateControlSystem.Server.Services.MediatR.Handlers.PredictionEngine
{
    public sealed class GetPredictionHandler : IRequestHandler<GetPredictionQuery, Prediction>
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
            var tensorPredictionRequest = _mapper.Map<TensorPredictionRequest>(request.FeaturesData);

            var tensorPredictionResult = await _predictionEngine.Predict(tensorPredictionRequest);

            var prediction = _mapper.Map<Prediction>(tensorPredictionResult);

            prediction.Features = request.FeaturesData;

            return prediction;
        }
    }
}
