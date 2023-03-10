using AutoMapper;
using MediatR;
using Shared.Dtos;
using WebApi.Infrastructure.Repositories;
using WebApi.Services.MediatR.Queries.MonitoringsRepository;

namespace WebApi.Services.MediatR.Handlers.MonitoringsRepository
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

                var microclimatesDto = monitoringsEntities.Select(entity => _mapper.Map<ForecastingDto>(entity)).ToList();

                return microclimatesDto;
            }
            catch (Exception)
            {
                return new List<ForecastingDto>();
            }
        }
    }
}
