using MediatR;

namespace Application.MediatR.ForecastRepository
{
    public sealed class GetWarningsCountQuery : IRequest<long>
    {
    }
}
