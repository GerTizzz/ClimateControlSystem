using MediatR;

namespace WebApi.Services.MediatR.Queries.MonitoringsRepository
{
    public sealed class GetMicroclimatesEventsCountQuery : IRequest<long>
    {
    }
}
