using MediatR;
using Shared.Dtos;
using WebApi.Resources.Infrastructure;

namespace WebApi.Services.MediatR.Queries.MonitoringsRepository
{
    public sealed class GetMonitoringEventsQuery : IRequest<List<MonitoringsEventsDto>>
    {
        public RequestLimits RequestLimits { get; }

        public GetMonitoringEventsQuery(RequestLimits requestLimits)
        {
            RequestLimits = requestLimits;
        }
    }
}
