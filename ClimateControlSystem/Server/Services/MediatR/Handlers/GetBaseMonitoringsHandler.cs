using AutoMapper;
using ClimateControlSystem.Server.Domain.Repositories;
using ClimateControlSystem.Server.Services.MediatR.Queries;
using ClimateControlSystem.Shared.SendToClient;
using MediatR;

namespace ClimateControlSystem.Server.Services.MediatR.Handlers
{
    public sealed class GetBaseMonitoringsHandler : IRequestHandler<GetBaseMonitoringsQuery, List<BaseMonitoringDTO>>
    {
        private readonly IMapper _mapper;
        private readonly IMicroclimateRepository _microclimateRepository;

        public GetBaseMonitoringsHandler(IMapper mapper, IMicroclimateRepository microclimateRepository)
        {
            _mapper = mapper;
            _microclimateRepository = microclimateRepository;
        }

        public async Task<List<BaseMonitoringDTO>> Handle(GetBaseMonitoringsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var baseMonitoringsEntities = await _microclimateRepository.GetBaseMonitoringsAsync(request.RequestLimits);

                var baseMonitoringsDTO = baseMonitoringsEntities.Select(entity => _mapper.Map<BaseMonitoringDTO>(entity)).ToList();

                return baseMonitoringsDTO;
            }
            catch (Exception ex)
            {
                return new List<BaseMonitoringDTO>();
            }
        }
    }
}
