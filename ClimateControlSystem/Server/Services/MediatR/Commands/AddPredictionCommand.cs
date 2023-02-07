using ClimateControlSystem.Server.Resources.Common;
using MediatR;

namespace ClimateControlSystem.Server.Services.MediatR.Commands
{
    public class AddPredictionCommand : IRequest<bool>
    {
        public PredictionResult Predicition { get; init; }
        public TemperatureEvent? TemperatureEvent { get; init; }
        public HumidityEvent? HumidityEvent { get; init; }
    }
}
