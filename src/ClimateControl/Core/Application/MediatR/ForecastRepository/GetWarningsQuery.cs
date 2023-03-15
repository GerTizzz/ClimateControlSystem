using Application.Primitives;
using MediatR;
using Shared.Dtos;

namespace Application.MediatR.ForecastRepository
{
    public sealed class GetWarningsQuery : IRequest<List<WarningDto>>
    {
        public DbRequest RequestLimits { get; }

        public GetWarningsQuery(DbRequest requestLimits)
        {
            RequestLimits = requestLimits;
        }
    }
}
