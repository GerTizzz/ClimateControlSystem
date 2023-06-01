using Application.Primitives;
using MediatR;
using Shared.Dtos;

namespace Application.MediatR.WarningsRepository;

public sealed class GetWarningsQuery : IRequest<List<WarningDto>>
{
    public DbRangeRequest RangeRequestLimits { get; }

    public GetWarningsQuery(DbRangeRequest rangeRequestLimits)
    {
        RangeRequestLimits = rangeRequestLimits;
    }
}