using ClimateControlSystem.Server.Domain.Repositories;
using ClimateControlSystem.Server.Services.MediatR.Queries.MonitoringsRepository;
using MediatR;

namespace ClimateControlSystem.Server.Services.MediatR.Handlers.MicroclimateRepository
{
    public sealed class GetMicroclimatesEventsCountHandler : IRequestHandler<GetMicroclimatesEventsCountQuery, long>
    {
        private readonly IMonitoringsRepository _microclimateRepository;

        public GetMicroclimatesEventsCountHandler(IMonitoringsRepository microclimateRepository)
        {
            _microclimateRepository = microclimateRepository;
        }

        public async Task<long> Handle(GetMicroclimatesEventsCountQuery request, CancellationToken cancellationToken)
        {
            return await _microclimateRepository.GetMicroclimatesEventsCountAsync();
        }
    }
}
