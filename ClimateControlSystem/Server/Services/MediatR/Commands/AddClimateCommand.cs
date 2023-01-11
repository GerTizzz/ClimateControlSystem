using ClimateControlSystem.Server.Resources.Common;
using ClimateControlSystem.Shared.Common;
using MediatR;

namespace ClimateControlSystem.Server.Services.MediatR.Commands
{
    public class AddClimateCommand : IRequest<bool>
    {
        public PredictionResult Prediction { get; init; }
        public SensorsData SensorData { get; init; }
        public TemperatureEvent TemperatureEvent { get; init; }
        public HumidityEvent HumidityEvent { get; init; }
    }
}
