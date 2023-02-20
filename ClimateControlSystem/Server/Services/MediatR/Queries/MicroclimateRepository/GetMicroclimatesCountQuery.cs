using MediatR;

namespace ClimateControlSystem.Server.Services.MediatR.Queries.MicroclimateRepository
{
    public sealed class GetMicroclimatesCountQuery : IRequest<long>
    {
    }
}
