using MediatR;
using Shared.Dtos;
using WebApi.Resources.Infrastructure;

namespace WebApi.Services.MediatR.Queries.MonitoringsRepository
{
    public sealed class GetForecastsDtosQuery : IRequest<List<ForecastingDto>>
    {
        public DbRequest RequestLimits { get; }

        public GetForecastsDtosQuery(DbRequest requestLimits)
        {
            RequestLimits = requestLimits;
        }
    }
}
