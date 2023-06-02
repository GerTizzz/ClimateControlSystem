using Application.Primitives;
using Application.Services.Strategies;
using AutoMapper;
using Domain.Repositories;
using MediatR;

namespace Application.MediatR.PredictionEngine;

public sealed class GetPredictionHandler : IRequestHandler<GetPredictionQuery, List<PredictedValue>>
{
    private readonly IPredictionEngine _predictionEngine;
    private readonly IMapper _mapper;
    private readonly IForecastsRepository _forecastsRepository;

    public GetPredictionHandler(IPredictionEngine predictionEngine, IMapper mapper, IForecastsRepository forecastsRepository)
    {
        _predictionEngine = predictionEngine;
        _mapper = mapper;
        _forecastsRepository = forecastsRepository;
    }

    public async Task<List<PredictedValue>> Handle(GetPredictionQuery request, CancellationToken cancellationToken)
    {
        var featuresData = await GetFeatures(request.Feature);

        if (featuresData.Count < TensorSettings.InputSize)
        {
            return Enumerable.Empty<PredictedValue>().ToList();
        }

        var tensorPredictionRequest = new TensorRequest()
        {
            serving_default_lstm_input = featuresData.ToArray()
        };

        var tensorPredictionResult = await _predictionEngine.Predict(tensorPredictionRequest);

        var prediction = _mapper.Map<IEnumerable<PredictedValue>>(tensorPredictionResult).ToList();

        return prediction;
    }

    private async Task<List<float>> GetFeatures(Feature newFeature)
    {
        var features = new List<float>();

        foreach (var feature in await GetLastFeatures())
        {
            features.Add(feature.TemperatureOutside);
            features.Add(feature.TemperatureInside);
            features.Add(feature.CoolingPower);
        }

        features.Add(newFeature.TemperatureOutside);
        features.Add(newFeature.TemperatureInside);
        features.Add(newFeature.CoolingPower);

        return features;
    }

    private async Task<IEnumerable<Feature>> GetLastFeatures()
    {
        return await _forecastsRepository.GetLastFeatures(new DbRangeRequest(0, TensorSettings.NumberOfDataSets - 1));
    }
}