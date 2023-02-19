using ClimateControlSystem.Server.Infrastructure;
using ClimateControlSystem.Shared.SendToClient;
using MediatR;

namespace ClimateControlSystem.Server.Services.MediatR.Queries
{
    public sealed class GetBaseMonitoringsQuery : IRequest<List<BaseMonitoringDTO>>
    {
        public RequestLimits RequestLimits { get; }

        public GetBaseMonitoringsQuery(RequestLimits requestLimits)
        {
            RequestLimits = requestLimits;
        }
    }
}
