using Application.Primitives;
using Application.Services.Strategies;
using AutoMapper;
using MediatR;

namespace Application.MediatR.PredictionEngine
{
    public sealed class GetPredictionHandler : IRequestHandler<GetPredictionQuery, Label>
    {
        private readonly IPredictionEngineService _predictionEngine;
        private readonly IMapper _mapper;

        public GetPredictionHandler(IPredictionEngineService predictionEngine, IMapper mapper)
        {
            _predictionEngine = predictionEngine;
            _mapper = mapper;
        }

        public async Task<Label> Handle(GetPredictionQuery request, CancellationToken cancellationToken)
        {
            var tensorPredictionRequest = _mapper.Map<TensorPredictionRequest>(request.Feature);

            var tensorPredictionResult = await _predictionEngine.Predict(tensorPredictionRequest);

            var prediction = _mapper.Map<Label>(tensorPredictionResult);

            return prediction;
        }
    }
}
