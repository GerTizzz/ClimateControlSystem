using MediatR;

namespace Application.MediatR.ForecastRepository
{
    public sealed class SaveForecastCommand : IRequest<bool>
    {
        public Forecast Forecast { get; }

        public SaveForecastCommand(Forecast forecast)
        {
            Forecast = forecast;
        }
    }
}
