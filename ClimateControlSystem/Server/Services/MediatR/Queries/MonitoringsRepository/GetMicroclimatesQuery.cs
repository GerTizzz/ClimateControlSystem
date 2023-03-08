using ClimateControl.Server.Resources.Infrastructure;
using ClimateControl.Shared.Dtos;
using MediatR;

namespace ClimateControl.Server.Services.MediatR.Queries.MonitoringsRepository
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
