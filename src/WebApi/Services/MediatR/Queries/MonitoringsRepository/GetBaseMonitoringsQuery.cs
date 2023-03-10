using MediatR;
using Shared.Dtos;
using WebApi.Resources.Infrastructure;

namespace WebApi.Services.MediatR.Queries.MonitoringsRepository
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
