using Application.Primitives;
using Application.Services.Strategies;
using AutoMapper;
using MediatR;

namespace Application.MediatR.PredictionEngine;

public sealed class GetPredictionHandler : IRequestHandler<GetPredictionQuery, PredictedValue>
{
    private readonly IPredictionEngine _predictionEngine;
    private readonly IMapper _mapper;

    public GetPredictionHandler(IPredictionEngine predictionEngine, IMapper mapper)
    {
        _predictionEngine = predictionEngine;
        _mapper = mapper;
    }

    public async Task<PredictedValue> Handle(GetPredictionQuery request, CancellationToken cancellationToken)
    {
        var tensorPredictionRequest = _mapper.Map<TensorPredictionRequest>(request.Feature);

        var tensorPredictionResult = await _predictionEngine.Predict(tensorPredictionRequest);

        var prediction = _mapper.Map<PredictedValue>(tensorPredictionResult);

        return prediction;
    }
}