using AutoMapper;
using Domain.Repositories;
using MediatR;
using Shared.Dtos;

namespace Application.MediatR.ForecastRepository
{
    public sealed class GetForecastsHandler : IRequestHandler<GetForecastsQuery, List<ForecastDto>>
    {
        private readonly IMapper _mapper;
        private readonly IForecastRepository _microclimateRepository;

        public GetForecastsHandler(IMapper mapper, IForecastRepository microclimateRepository)
        {
            _mapper = mapper;
            _microclimateRepository = microclimateRepository;
        }

        public async Task<List<ForecastDto>> Handle(GetForecastsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var forecasts = await _microclimateRepository.GetForecastsAsync(request.RequestLimits);

                var forecastsDtos = forecasts.Select(entity => _mapper.Map<ForecastDto>(entity)).ToList();

                return forecastsDtos;
            }
            catch (Exception)
            {
                return new List<ForecastDto>();
            }
        }
    }
}
