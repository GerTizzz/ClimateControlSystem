using Application.gRCP.Protos;
using MediatR;

namespace Application.MediatR.ForecastService
{
    public sealed class ProcessMicroclimateQuery : IRequest<Label>
    {
        public ClimateMonitoringRequest MonitoringRequest { get; }

        public ProcessMicroclimateQuery(ClimateMonitoringRequest monitoringRequest)
        {
            MonitoringRequest = monitoringRequest;
        }
    }
}
