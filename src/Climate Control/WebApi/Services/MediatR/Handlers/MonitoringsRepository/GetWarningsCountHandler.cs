using MediatR;
using WebApi.Infrastructure.Repositories;
using WebApi.Services.MediatR.Queries.MonitoringsRepository;

namespace WebApi.Services.MediatR.Handlers.MonitoringsRepository
{
    public sealed class GetWarningsCountHandler : IRequestHandler<GetWarningsCountQuery, long>
    {
        private readonly IMonitoringsRepository _microclimateRepository;

        public GetWarningsCountHandler(IMonitoringsRepository microclimateRepository)
        {
            _microclimateRepository = microclimateRepository;
        }

        public async Task<long> Handle(GetWarningsCountQuery request, CancellationToken cancellationToken)
        {
            return await _microclimateRepository.GetMicroclimatesEventsCountAsync();
        }
    }
}
