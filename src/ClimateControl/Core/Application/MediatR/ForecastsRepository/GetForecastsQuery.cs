using Application.Primitives;
using MediatR;
using Shared.Dtos;

namespace Application.MediatR.ForecastsRepository;

public sealed class GetForecastsQuery : IRequest<List<ForecastDto>>
{
    public DbRangeRequest RangeRequestLimits { get; }

    public GetForecastsQuery(DbRangeRequest rangeRequestLimits)
    {
        RangeRequestLimits = rangeRequestLimits;
    }
}