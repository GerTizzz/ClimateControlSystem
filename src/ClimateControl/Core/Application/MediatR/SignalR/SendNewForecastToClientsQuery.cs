using MediatR;

namespace Application.MediatR.SignalR;

public sealed class SendNewForecastToClientsQuery : IRequest<bool>
{
    public Forecast Forecast { get; }

    public SendNewForecastToClientsQuery(Forecast forecast)
    {
        Forecast = forecast;
    }
}