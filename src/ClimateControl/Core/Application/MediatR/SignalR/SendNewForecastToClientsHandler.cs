using AutoMapper;
using Infrastructure.SignalR;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Shared.Dtos;

namespace Application.MediatR.SignalR;

public sealed class SendNewForecastToClientsHandler : IRequestHandler<SendNewForecastToClientsQuery, bool>
{
    private readonly IHubContext<ForecastHub> _monitoringHub;
    private readonly IMapper _mapper;

    public SendNewForecastToClientsHandler(IMapper mapper, IHubContext<ForecastHub> monitoringHub)
    {
        _mapper = mapper;
        _monitoringHub = monitoringHub;
    }

    public async Task<bool> Handle(SendNewForecastToClientsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var forecastDto = _mapper.Map<ForecastDto>(request.Forecast);

            await _monitoringHub.Clients.All.SendAsync("GetNewForecast", forecastDto, cancellationToken: cancellationToken);

            return true;
        }
        catch
        {
            return false;
        }
    }
}