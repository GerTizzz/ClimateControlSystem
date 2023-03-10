using MediatR;
using Shared.Dtos;
using WebApi.Resources.Infrastructure;

namespace WebApi.Services.MediatR.Queries.MonitoringsRepository
{
    public sealed class GetMicroclimatesQuery : IRequest<List<ForecastingDto>>
    {
        public RequestLimits RequestLimits { get; }

        public GetMicroclimatesQuery(RequestLimits requestLimits)
        {
            RequestLimits = requestLimits;
        }
    }
}
