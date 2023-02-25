using AutoMapper;
using ClimateControlSystem.Server.Domain.Repositories;
using ClimateControlSystem.Server.Services.MediatR.Queries.MicroclimateRepository;
using ClimateControlSystem.Shared.Responses;
using MediatR;

namespace ClimateControlSystem.Server.Services.MediatR.Handlers.MicroclimateRepository
{
    public sealed class GetMonitoringEventsHandler : IRequestHandler<GetMonitoringEventsQuery, List<MonitoringsEventsDto>>
    {
        private readonly IMapper _mapper;
        private readonly IMicroclimateRepository _microclimateRepository;

        public GetMonitoringEventsHandler(IMapper mapper, IMicroclimateRepository microclimateRepository)
        {
            _mapper = mapper;
            _microclimateRepository = microclimateRepository;
        }

        public async Task<List<MonitoringsEventsDto>> Handle(GetMonitoringEventsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var monitoringsEntities = await _microclimateRepository.GetMonitoringEventsAsync(request.RequestLimits);

                var monitoringsDto = monitoringsEntities.Select(entity => _mapper.Map<MonitoringsEventsDto>(entity)).ToList();

                return monitoringsDto;
            }
            catch (Exception ex)
            {
                return new List<MonitoringsEventsDto>();
            }
        }
    }
}
