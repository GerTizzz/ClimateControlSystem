using Application.Primitives;
using MediatR;
using Shared.Dtos;

namespace Application.MediatR.ForecastsRepository;

public sealed class GetFeaturesQuery : IRequest<List<FeaturesDto>>
{
    public DbRangeRequest RangeRequestLimits { get; }

    public GetFeaturesQuery(DbRangeRequest rangeRequestLimits)
    {
        RangeRequestLimits = rangeRequestLimits;
    }
}
