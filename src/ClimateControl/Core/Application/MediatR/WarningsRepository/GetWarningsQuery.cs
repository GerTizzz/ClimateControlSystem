using Application.Primitives;
using MediatR;
using Shared.Dtos;

namespace Application.MediatR.WarningsRepository;

public sealed class GetWarningsQuery : IRequest<List<WarningDto>>
{
    public DbRequest RequestLimits { get; }

    public GetWarningsQuery(DbRequest requestLimits)
    {
        RequestLimits = requestLimits;
    }
}