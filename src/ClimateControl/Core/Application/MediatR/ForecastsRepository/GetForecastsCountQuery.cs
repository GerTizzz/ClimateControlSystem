using MediatR;

namespace Application.MediatR.ForecastsRepository;

public sealed class GetForecastsCountQuery : IRequest<long>
{
}