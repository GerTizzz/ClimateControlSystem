using Domain.Repositories;
using MediatR;

namespace Application.MediatR.ForecastsRepository;

public sealed class SaveForecastHandler : IRequestHandler<SaveForecastCommand, bool>
{
    private readonly IForecastsRepository _predictionRepository;

    public SaveForecastHandler(IForecastsRepository predictionRepository)
    {
        _predictionRepository = predictionRepository;
    }

    public async Task<bool> Handle(SaveForecastCommand request, CancellationToken cancellationToken)
    {
        return await _predictionRepository.SaveForecastAsync(request.Forecast);
    }
}