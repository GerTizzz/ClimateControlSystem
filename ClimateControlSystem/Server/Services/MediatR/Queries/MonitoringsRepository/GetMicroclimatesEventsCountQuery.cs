using MediatR;

namespace ClimateControlSystem.Server.Services.MediatR.Queries.MonitoringsRepository
{
    public sealed class GetMicroclimatesEventsCountQuery : IRequest<long>
    {
    }
}
