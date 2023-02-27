using ClimateControlSystem.Server.Infrastructure;
using ClimateControlSystem.Shared.Responses;
using MediatR;

namespace ClimateControlSystem.Server.Services.MediatR.Queries.MonitoringsRepository
{
    public sealed class GetBaseMonitoringsQuery : IRequest<List<BaseMonitoringDto>>
    {
        public RequestLimits RequestLimits { get; }

        public GetBaseMonitoringsQuery(RequestLimits requestLimits)
        {
            RequestLimits = requestLimits;
        }
    }
}
