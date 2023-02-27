using ClimateControlSystem.Server.Domain.Repositories;
using ClimateControlSystem.Server.Services.MediatR.Queries.MonitoringsRepository;
using MediatR;

namespace ClimateControlSystem.Server.Services.MediatR.Handlers.MonitoringsRepository
{
    public sealed class GetMonitoringsCountHandler : IRequestHandler<GetMonitoringsCountQuery, long>
    {
        private readonly IMonitoringsRepository _microclimateRepository;

        public GetMonitoringsCountHandler(IMonitoringsRepository microclimateRepository)
        {
            _microclimateRepository = microclimateRepository;
        }

        public async Task<long> Handle(GetMonitoringsCountQuery request, CancellationToken cancellationToken)
        {
            return await _microclimateRepository.GetMonitoringsCountAsync();
        }
    }
}
