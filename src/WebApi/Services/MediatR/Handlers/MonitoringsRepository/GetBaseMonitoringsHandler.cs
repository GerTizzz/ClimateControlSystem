using AutoMapper;
using MediatR;
using Shared.Dtos;
using WebApi.Infrastructure.Repositories;
using WebApi.Services.MediatR.Queries.MonitoringsRepository;

namespace WebApi.Services.MediatR.Handlers.MonitoringsRepository
{
    public sealed class GetBaseMonitoringsHandler : IRequestHandler<GetBaseMonitoringsQuery, List<BaseMonitoringDto>>
    {
        private readonly IMapper _mapper;
        private readonly IMonitoringsRepository _microclimateRepository;

        public GetBaseMonitoringsHandler(IMapper mapper, IMonitoringsRepository microclimateRepository)
        {
            _mapper = mapper;
            _microclimateRepository = microclimateRepository;
        }

        public async Task<List<BaseMonitoringDto>> Handle(GetBaseMonitoringsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var monitoringsEntities = await _microclimateRepository.GetBaseMonitoringsAsync(request.RequestLimits);

                var monitoringsDto = monitoringsEntities.Select(entity => _mapper.Map<BaseMonitoringDto>(entity)).ToList();

                return monitoringsDto;
            }
            catch (Exception)
            {
                return new List<BaseMonitoringDto>();
            }
        }
    }
}
