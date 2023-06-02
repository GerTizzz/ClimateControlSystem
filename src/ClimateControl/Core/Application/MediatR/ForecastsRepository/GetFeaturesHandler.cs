using AutoMapper;
using Domain.Repositories;
using MediatR;
using Shared.Dtos;

namespace Application.MediatR.ForecastsRepository;

public sealed class GetFeaturesHandler : IRequestHandler<GetFeaturesQuery, List<FeaturesDto>>
{
    private readonly IMapper _mapper;
    private readonly IForecastsRepository _microclimateRepository;

    public GetFeaturesHandler(IMapper mapper, IForecastsRepository microclimateRepository)
    {
        _mapper = mapper;
        _microclimateRepository = microclimateRepository;
    }

    public async Task<List<FeaturesDto>> Handle(GetFeaturesQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var features = await _microclimateRepository.GetLastFeatures(request.RangeRequestLimits);

            var featuresDtos = features.Select(entity => _mapper.Map<FeaturesDto>(entity)).ToList();

            return featuresDtos;
        }
        catch (Exception)
        {
            return new List<FeaturesDto>();
        }
    }
}
