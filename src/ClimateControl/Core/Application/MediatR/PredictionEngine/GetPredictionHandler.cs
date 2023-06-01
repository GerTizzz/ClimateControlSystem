using Application.Primitives;
using Application.Services.Strategies;
using AutoMapper;
using MediatR;

namespace Application.MediatR.PredictionEngine;

public sealed class GetPredictionHandler : IRequestHandler<GetPredictionQuery, List<PredictedValue>>
{
    private readonly IPredictionEngine _predictionEngine;
    private readonly IMapper _mapper;

    public GetPredictionHandler(IPredictionEngine predictionEngine, IMapper mapper)
    {
        _predictionEngine = predictionEngine;
        _mapper = mapper;
    }

    public async Task<List<PredictedValue>> Handle(GetPredictionQuery request, CancellationToken cancellationToken)
    {
        var featuresData = new List<float>();

        foreach (var feature in request.Features)
        {
            featuresData.Add(feature.TemperatureOutside);
            featuresData.Add(feature.TemperatureInside);
            featuresData.Add(feature.CoolingPower);
        }

        var tensorPredictionRequest = new TensorPredictionRequest()
        {
            serving_default_lstm_input = featuresData.ToArray()
        };

        var tensorPredictionResult = await _predictionEngine.Predict(tensorPredictionRequest);

        var prediction = _mapper.Map<List<PredictedValue>>(tensorPredictionResult);

        return prediction;
    }
}