using ClimateControlSystem.Server.Infrastructure;
using ClimateControlSystem.Shared.Responses;
using MediatR;

namespace ClimateControlSystem.Server.Services.MediatR.Queries.MonitoringsRepository
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
