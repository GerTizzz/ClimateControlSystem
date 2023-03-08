using ClimateControl.Server.Resources.Infrastructure;
using ClimateControl.Shared.Dtos;
using MediatR;

namespace ClimateControl.Server.Services.MediatR.Queries.MonitoringsRepository
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
