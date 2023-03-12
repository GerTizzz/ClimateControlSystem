using Application.Primitives;
using MediatR;
using Shared.Dtos;

namespace Application.MediatR.ForecastRepository
{
    public sealed class GetBaseMonitoringsQuery : IRequest<List<BaseMonitoringDto>>
    {
        public DbRequest RequestLimits { get; }

        public GetBaseMonitoringsQuery(DbRequest requestLimits)
        {
            RequestLimits = requestLimits;
        }
    }
}
