using ClimateControl.Server.Resources.Infrastructure;
using ClimateControl.Shared.Dtos;
using MediatR;

namespace ClimateControl.Server.Services.MediatR.Queries.MonitoringsRepository
{
    public sealed class GetMonitoringsWithAccuracyQuery : IRequest<List<MonitoringWithAccuracyDto>>
    {
        public RequestLimits RequestLimits { get; }

        public GetMonitoringsWithAccuracyQuery(RequestLimits requestLimits)
        {
            RequestLimits = requestLimits;
        }
    }
}
