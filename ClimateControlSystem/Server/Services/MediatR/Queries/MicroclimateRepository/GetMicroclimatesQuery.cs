using ClimateControlSystem.Server.Infrastructure;
using ClimateControlSystem.Shared.Responses;
using MediatR;

namespace ClimateControlSystem.Server.Services.MediatR.Queries.MicroclimateRepository
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
