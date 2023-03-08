using AutoMapper;
using ClimateControl.Server.Infrastructure.Repositories;
using ClimateControl.Server.Services.MediatR.Queries.MonitoringsRepository;
using ClimateControl.Shared.Dtos;
using MediatR;

namespace ClimateControl.Server.Services.MediatR.Handlers.MonitoringsRepository
{
    public sealed class GetMicroclimatesHandler : IRequestHandler<GetMicroclimatesQuery, List<ForecastingDto>>
    {
        private readonly IMapper _mapper;
        private readonly IMonitoringsRepository _microclimateRepository;

        public GetMicroclimatesHandler(IMapper mapper, IMonitoringsRepository microclimateRepository)
        {
            _mapper = mapper;
            _microclimateRepository = microclimateRepository;
        }

        public async Task<List<ForecastingDto>> Handle(GetMicroclimatesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var monitoringsEntities = await _microclimateRepository.GetMicroclimatesAsync(request.RequestLimits);

                var microclimatesDTO = monitoringsEntities.Select(entity => _mapper.Map<ForecastingDto>(entity)).ToList();

                return microclimatesDTO;
            }
            catch (Exception ex)
            {
                return new List<ForecastingDto>();
            }
        }
    }
}
