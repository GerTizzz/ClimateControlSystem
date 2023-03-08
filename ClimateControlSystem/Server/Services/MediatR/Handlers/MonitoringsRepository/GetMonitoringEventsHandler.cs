using AutoMapper;
using ClimateControl.Server.Infrastructure.Repositories;
using ClimateControl.Server.Services.MediatR.Queries.MonitoringsRepository;
using ClimateControl.Shared.Dtos;
using MediatR;

namespace ClimateControl.Server.Services.MediatR.Handlers.MonitoringsRepository
{
    public sealed class GetMonitoringEventsHandler : IRequestHandler<GetMonitoringEventsQuery, List<MonitoringsEventsDto>>
    {
        private readonly IMapper _mapper;
        private readonly IMonitoringsRepository _microclimateRepository;

        public GetMonitoringEventsHandler(IMapper mapper, IMonitoringsRepository microclimateRepository)
        {
            _mapper = mapper;
            _microclimateRepository = microclimateRepository;
        }

        public async Task<List<MonitoringsEventsDto>> Handle(GetMonitoringEventsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var monitoringsEntities = await _microclimateRepository.GetMicroclimatesEventsAsync(request.RequestLimits);

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
