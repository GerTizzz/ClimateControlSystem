using ClimateControl.Server.Infrastructure.Repositories;
using ClimateControl.Server.Services.MediatR.Queries.MonitoringsRepository;
using MediatR;

namespace ClimateControl.Server.Services.MediatR.Handlers.MonitoringsRepository
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
