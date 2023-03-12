using AutoMapper;
using Domain.Repositories;
using MediatR;
using Shared.Dtos;

namespace Application.MediatR.ForecastRepository
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
                var monitoringsEntities = await _microclimateRepository.GetForecastsAsync(request.RequestLimits);

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
