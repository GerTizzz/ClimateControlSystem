using Domain.Repositories;
using MediatR;

namespace Application.MediatR.ForecastRepository
{
    public sealed class GetWarningsCountHandler : IRequestHandler<GetWarningsCountQuery, long>
    {
        private readonly IForecastRepository _microclimateRepository;

        public GetWarningsCountHandler(IForecastRepository microclimateRepository)
        {
            _microclimateRepository = microclimateRepository;
        }

        public async Task<long> Handle(GetWarningsCountQuery request, CancellationToken cancellationToken)
        {
            return await _microclimateRepository.GetMicroclimatesEventsCountAsync();
        }
    }
}
