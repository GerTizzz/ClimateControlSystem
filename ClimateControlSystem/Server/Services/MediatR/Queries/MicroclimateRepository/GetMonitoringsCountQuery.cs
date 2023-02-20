using MediatR;

namespace ClimateControlSystem.Server.Services.MediatR.Queries.MicroclimateRepository
{
    public sealed class GetMonitoringsCountQuery : IRequest<long>
    {
    }
}
