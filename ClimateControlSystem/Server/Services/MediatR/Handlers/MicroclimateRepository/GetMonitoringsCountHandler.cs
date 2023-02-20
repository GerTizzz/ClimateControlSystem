using ClimateControlSystem.Server.Domain.Repositories;
using ClimateControlSystem.Server.Services.MediatR.Queries.MicroclimateRepository;
using MediatR;

namespace ClimateControlSystem.Server.Services.MediatR.Handlers.MicroclimateRepository
{
    public sealed class GetMonitoringsCountHandler : IRequestHandler<GetMonitoringsCountQuery, long>
    {
        private readonly IMicroclimateRepository _microclimateRepository;

        public GetMonitoringsCountHandler(IMicroclimateRepository microclimateRepository)
        {
            _microclimateRepository = microclimateRepository;
        }

        public async Task<long> Handle(GetMonitoringsCountQuery request, CancellationToken cancellationToken)
        {
            return await _microclimateRepository.GetMonitoringsCountAsync();
        }
    }
}
