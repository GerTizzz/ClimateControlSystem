using MediatR;

namespace Application.MediatR.ForecastRepository
{
    public sealed class GetForecastsCountQuery : IRequest<long>
    {
    }
}
