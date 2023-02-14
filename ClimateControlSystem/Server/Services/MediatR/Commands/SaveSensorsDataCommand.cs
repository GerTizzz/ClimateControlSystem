using ClimateControlSystem.Server.Resources.Common;
using MediatR;

namespace ClimateControlSystem.Server.Services.MediatR.Commands
{
    public sealed class SaveSensorsDataCommand : IRequest<bool>
    {
        public SensorsData SensorsData { get; }

        public SaveSensorsDataCommand(SensorsData sensorsData)
        {
            SensorsData = sensorsData;
        }
    }
}
