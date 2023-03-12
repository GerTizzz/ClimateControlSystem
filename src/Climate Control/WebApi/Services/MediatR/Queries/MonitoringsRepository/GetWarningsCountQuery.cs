using MediatR;

namespace WebApi.Services.MediatR.Queries.MonitoringsRepository
{
    public sealed class GetWarningsCountQuery : IRequest<long>
    {
    }
}
