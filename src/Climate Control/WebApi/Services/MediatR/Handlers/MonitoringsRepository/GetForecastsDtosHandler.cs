using AutoMapper;
using MediatR;
using Shared.Dtos;
using WebApi.Infrastructure.Repositories;
using WebApi.Services.MediatR.Queries.MonitoringsRepository;

namespace WebApi.Services.MediatR.Handlers.MonitoringsRepository
{
    public sealed class GetForecastsDtosHandler : IRequestHandler<GetForecastsDtosQuery, List<ForecastingDto>>
    {
        private readonly IMapper _mapper;
        private readonly IMonitoringsRepository _microclimateRepository;

        public GetForecastsDtosHandler(IMapper mapper, IMonitoringsRepository microclimateRepository)
        {
            _mapper = mapper;
            _microclimateRepository = microclimateRepository;
        }

        public async Task<List<ForecastingDto>> Handle(GetForecastsDtosQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var forecasts = await _microclimateRepository.GetForecastsAsync(request.RequestLimits);

                var forecastsDto = forecasts
                    .Select(entity => _mapper.Map<ForecastingDto>(entity))
                    .ToList();

                return forecastsDto;
            }
            catch (Exception)
            {
                return new List<ForecastingDto>();
            }
        }
    }
}
