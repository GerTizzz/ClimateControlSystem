using Application.Primitives;
using AutoMapper;
using Domain.Repositories;
using MediatR;
using Shared.Dtos;

namespace Application.MediatR.ForecastsRepository
{
    public sealed class GetForecastHandler : IRequestHandler<GetForecastQuery, ForecastDto?>
    {
        private readonly IMapper _mapper;
        private readonly IForecastsRepository _forecastsRepository;

        public GetForecastHandler(IMapper mapper, IForecastsRepository forecastsRepository)
        {
            _mapper = mapper;
            _forecastsRepository = forecastsRepository;
        }

        public async Task<ForecastDto?> Handle(GetForecastQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var forecast = await _forecastsRepository.GetForecastsAsync(new DbRangeRequest(request.Number, 1));

                if (forecast is null)
                {
                    return null;
                }

                var forecastDto = forecast
                    .Select(entity => _mapper.Map<ForecastDto>(entity))
                    .ToList()
                    .FirstOrDefault();

                return forecastDto;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
