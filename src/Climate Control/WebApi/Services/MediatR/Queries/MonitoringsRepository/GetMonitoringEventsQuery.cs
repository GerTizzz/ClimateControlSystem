using MediatR;
using Shared.Dtos;
using WebApi.Resources.Infrastructure;

namespace WebApi.Services.MediatR.Queries.MonitoringsRepository
{
    public sealed class GetMonitoringEventsQuery : IRequest<List<MonitoringsEventsDto>>
    {
        public DbRequest RequestLimits { get; }

        public GetMonitoringEventsQuery(DbRequest requestLimits)
        {
            RequestLimits = requestLimits;
        }
    }
}
