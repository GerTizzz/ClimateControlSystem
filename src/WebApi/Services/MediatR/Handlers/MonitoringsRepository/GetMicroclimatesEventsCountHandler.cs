using MediatR;
using WebApi.Infrastructure.Repositories;
using WebApi.Services.MediatR.Queries.MonitoringsRepository;

namespace WebApi.Services.MediatR.Handlers.MonitoringsRepository
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
