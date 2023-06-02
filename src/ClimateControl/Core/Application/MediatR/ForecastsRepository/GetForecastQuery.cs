using MediatR;
using Shared.Dtos;

namespace Application.MediatR.ForecastsRepository
{
    public sealed class GetForecastQuery : IRequest<ForecastDto>
    {
        public int Number { get; }

        public GetForecastQuery(int number)
        {
            Number = number;
        }
    }
}
