using MediatR;
using Shared.Dtos;
using WebApi.Resources.Infrastructure;

namespace WebApi.Services.MediatR.Queries.MonitoringsRepository
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
