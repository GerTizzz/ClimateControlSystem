using Domain.Repositories;
using MediatR;

namespace Application.MediatR.ForecastsRepository;

public sealed class GetForecastsCountHandler : IRequestHandler<GetForecastsCountQuery, long>
{
    private readonly IForecastsRepository _microclimateRepository;

    public GetForecastsCountHandler(IForecastsRepository microclimateRepository)
    {
        _microclimateRepository = microclimateRepository;
    }

    public async Task<long> Handle(GetForecastsCountQuery request, CancellationToken cancellationToken)
    {
        return await _microclimateRepository.GetForecastsCountAsync();
    }
}