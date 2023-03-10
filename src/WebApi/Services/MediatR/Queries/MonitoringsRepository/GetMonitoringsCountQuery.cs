using MediatR;

namespace WebApi.Services.MediatR.Queries.MonitoringsRepository
{
    public sealed class GetMonitoringsCountQuery : IRequest<long>
    {
    }
}
