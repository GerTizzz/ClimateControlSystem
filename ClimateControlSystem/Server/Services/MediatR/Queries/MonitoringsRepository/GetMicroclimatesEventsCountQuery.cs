using MediatR;

namespace ClimateControl.Server.Services.MediatR.Queries.MonitoringsRepository
{
    public sealed class GetMicroclimatesEventsCountQuery : IRequest<long>
    {
    }
}
