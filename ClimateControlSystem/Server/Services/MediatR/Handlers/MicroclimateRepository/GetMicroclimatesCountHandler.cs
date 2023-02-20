using ClimateControlSystem.Server.Domain.Repositories;
using ClimateControlSystem.Server.Services.MediatR.Queries.MicroclimateRepository;
using MediatR;

namespace ClimateControlSystem.Server.Services.MediatR.Handlers.MicroclimateRepository
{
    public sealed class GetMicroclimatesCountHandler : IRequestHandler<GetMicroclimatesCountQuery, long>
    {
        private readonly IMicroclimateRepository _microclimateRepository;

        public GetMicroclimatesCountHandler(IMicroclimateRepository microclimateRepository)
        {
            _microclimateRepository = microclimateRepository;
        }

        public async Task<long> Handle(GetMicroclimatesCountQuery request, CancellationToken cancellationToken)
        {
            return await _microclimateRepository.GetMicroclimatesCountAsync();
        }
    }
}
