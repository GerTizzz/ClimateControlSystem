using ClimateControlSystem.Server.Infrastructure;
using ClimateControlSystem.Shared.Responses;
using MediatR;

namespace ClimateControlSystem.Server.Services.MediatR.Queries.MicroclimateRepository
{
    public sealed class GetMonitoringsWithAccuracyQuery : IRequest<List<MonitoringWithAccuracyDTO>>
    {
        public RequestLimits RequestLimits { get; }

        public GetMonitoringsWithAccuracyQuery(RequestLimits requestLimits)
        {
            RequestLimits = requestLimits;
        }
    }
}
