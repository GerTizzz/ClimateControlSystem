using ClimateControlSystem.Server.Resources.Common;
using ClimateControlSystem.Shared.Common;
using MediatR;

namespace ClimateControlSystem.Server.Services.MediatR.Commands
{
    public class AddClimateCommand : IRequest<bool>
    {
        public PredictionResultData Prediction { get; init; }
        public SensorsData SensorData { get; init; }
        public TemperatureEventData TemperatureEvent { get; init; }
        public HumidityEventData HumidityEvent { get; init; }
    }
}
