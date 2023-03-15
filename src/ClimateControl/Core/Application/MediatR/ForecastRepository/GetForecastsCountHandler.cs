using Domain.Repositories;
using MediatR;

namespace Application.MediatR.ForecastRepository
{
    public sealed class GetForecastsCountHandler : IRequestHandler<GetForecastsCountQuery, long>
    {
        private readonly IForecastRepository _microclimateRepository;

        public GetForecastsCountHandler(IForecastRepository microclimateRepository)
        {
            _microclimateRepository = microclimateRepository;
        }

        public async Task<long> Handle(GetForecastsCountQuery request, CancellationToken cancellationToken)
        {
            return await _microclimateRepository.GetWarningsCountAsync();
        }
    }
}
