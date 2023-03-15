using AutoMapper;
using Domain.Repositories;
using MediatR;
using Shared.Dtos;

namespace Application.MediatR.ForecastRepository
{
    public sealed class GetWarningsHandler : IRequestHandler<GetWarningsQuery, List<WarningDto>>
    {
        private readonly IMapper _mapper;
        private readonly IForecastRepository _forecastRepository;

        public GetWarningsHandler(IMapper mapper, IForecastRepository forecastRepository)
        {
            _mapper = mapper;
            _forecastRepository = forecastRepository;
        }

        public async Task<List<WarningDto>> Handle(GetWarningsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var warnings = await _forecastRepository.GetForecastsAsync(request.RequestLimits);

                var warningsDtos = warnings.Select(entity => _mapper.Map<WarningDto>(entity.Warning)).ToList();

                return warningsDtos;
            }
            catch (Exception)
            {
                return new List<WarningDto>();
            }
        }
    }
}
