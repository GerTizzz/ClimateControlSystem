using Application.Primitives;
using MediatR;
using Shared.Dtos;

namespace Application.MediatR.ForecastsRepository;

public sealed class GetForecastsQuery : IRequest<List<ForecastDto>>
{
    public DbRequest RequestLimits { get; }

    public GetForecastsQuery(DbRequest requestLimits)
    {
        RequestLimits = requestLimits;
    }
}