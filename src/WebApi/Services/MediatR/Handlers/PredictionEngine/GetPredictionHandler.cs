using AutoMapper;
using Domain.Entities;
using MediatR;
using WebApi.Infrastructure.Services;
using WebApi.Resources.PredictionEngine;
using WebApi.Services.MediatR.Queries.PredictionEngine;

namespace WebApi.Services.MediatR.Handlers.PredictionEngine
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
