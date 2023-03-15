using Application.gRCP.Protos;
using MediatR;

namespace Application.MediatR.ForecastService
{
    public sealed class ProcessMicroclimateQuery : IRequest<Label>
    {
        public GrpcForecastRequest ForecastRequest { get; }

        public ProcessMicroclimateQuery(GrpcForecastRequest forecastRequest)
        {
            ForecastRequest = forecastRequest;
        }
    }
}
