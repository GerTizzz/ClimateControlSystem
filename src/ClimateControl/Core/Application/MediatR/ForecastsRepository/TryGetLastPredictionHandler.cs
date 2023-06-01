using AutoMapper;
using Domain.Repositories;
using MediatR;

namespace Application.MediatR.ForecastsRepository;

public sealed class TryGetLastPredictionHandler : IRequestHandler<TryGetLastPredictionQuery, PredictedValue?>
{
    private readonly IForecastsRepository _predictionRepository;
    private readonly IMapper _mapper;

    public TryGetLastPredictionHandler(IForecastsRepository predictionRepository, IMapper mapper)
    {
        _predictionRepository = predictionRepository;
        _mapper = mapper;
    }

    public async Task<PredictedValue?> Handle(TryGetLastPredictionQuery request, CancellationToken cancellationToken)
    {
        var predictionRecord = await _predictionRepository.TryGetLastPredictionAsync();

        var prediction = _mapper.Map<PredictedValue>(predictionRecord);

        return prediction;
    }
}