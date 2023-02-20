using ClimateControlSystem.Server.Infrastructure;
using ClimateControlSystem.Shared.Responses;
using MediatR;

namespace ClimateControlSystem.Server.Services.MediatR.Queries.MicroclimateRepository
{
    public sealed class GetMonitoringEventsQuery : IRequest<List<MonitoringEventsDTO>>
    {
        public RequestLimits RequestLimits { get; }

        public GetMonitoringEventsQuery(RequestLimits requestLimits)
        {
            RequestLimits = requestLimits;
        }
    }
}
