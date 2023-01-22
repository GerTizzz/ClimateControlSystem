using ClimateControlSystem.Server.Resources.Common;
using MediatR;

namespace ClimateControlSystem.Server.Services.MediatR.Commands
{
    public class AddSensorsDataCommand : IRequest<bool>
    {
        public SensorsData SensorData { get; init; }
    }
}
