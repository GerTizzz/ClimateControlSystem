using MediatR;
using WebApi.Infrastructure.Repositories;
using WebApi.Services.MediatR.Queries.MonitoringsRepository;

namespace WebApi.Services.MediatR.Handlers.MonitoringsRepository
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
            return await _microclimateRepository.GetWarningsCountAsync();
        }
    }
}
