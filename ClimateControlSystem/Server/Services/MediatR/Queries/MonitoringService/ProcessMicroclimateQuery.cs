using ClimateControl.Server.Services.gRPC.Protos;
using MediatR;

namespace ClimateControl.Server.Services.MediatR.Queries.MonitoringService
{
    /// <summary>
    /// This query isn't supposed to return any result, but it does only to see the result in the client
    /// </summary>
    public sealed class ProcessMicroclimateQuery : IRequest<Prediction>
    {
        public ClimateMonitoringRequest MonitoringRequest { get; }

        public ProcessMicroclimateQuery(ClimateMonitoringRequest monitoringRequest)
        {
            MonitoringRequest = monitoringRequest;
        }
    }
}
