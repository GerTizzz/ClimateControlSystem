using ClimateControl.Server.Resources.Infrastructure;
using ClimateControl.Shared.Dtos;
using MediatR;

namespace ClimateControl.Server.Services.MediatR.Queries.MonitoringsRepository
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
