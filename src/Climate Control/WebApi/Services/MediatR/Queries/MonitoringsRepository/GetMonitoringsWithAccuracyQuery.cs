using MediatR;
using Shared.Dtos;
using WebApi.Resources.Infrastructure;

namespace WebApi.Services.MediatR.Queries.MonitoringsRepository
{
    public sealed class GetMonitoringsWithAccuracyQuery : IRequest<List<MonitoringWithAccuracyDto>>
    {
        public DbRequest RequestLimits { get; }

        public GetMonitoringsWithAccuracyQuery(DbRequest requestLimits)
        {
            RequestLimits = requestLimits;
        }
    }
}
