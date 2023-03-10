using AutoMapper;
using MediatR;
using Shared.Dtos;
using WebApi.Infrastructure.Repositories;
using WebApi.Services.MediatR.Queries.MonitoringsRepository;

namespace WebApi.Services.MediatR.Handlers.MonitoringsRepository
{
    public sealed class GetMonitoringsWithAccuracyHandler : IRequestHandler<GetMonitoringsWithAccuracyQuery, List<MonitoringWithAccuracyDto>>
    {
        private readonly IMapper _mapper;
        private readonly IMonitoringsRepository _microclimateRepository;

        public GetMonitoringsWithAccuracyHandler(IMapper mapper, IMonitoringsRepository microclimateRepository)
        {
            _mapper = mapper;
            _microclimateRepository = microclimateRepository;
        }

        public async Task<List<MonitoringWithAccuracyDto>> Handle(GetMonitoringsWithAccuracyQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var monitoringsEntities = await _microclimateRepository.GetMonitoringsWithAccuraciesAsync(request.RequestLimits);

                var monitoringsDto = monitoringsEntities.Select(entity => _mapper.Map<MonitoringWithAccuracyDto>(entity)).ToList();

                return monitoringsDto;
            }
            catch (Exception)
            {
                return new List<MonitoringWithAccuracyDto>();
            }
        }
    }
}
