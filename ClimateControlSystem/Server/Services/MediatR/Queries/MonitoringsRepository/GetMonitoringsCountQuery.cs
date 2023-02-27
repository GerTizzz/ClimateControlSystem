using MediatR;

namespace ClimateControlSystem.Server.Services.MediatR.Queries.MonitoringsRepository
{
    public sealed class GetMonitoringsCountQuery : IRequest<long>
    {
    }
}
