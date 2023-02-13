using ClimateControlSystem.Server.Resources.Common;
using MediatR;

namespace ClimateControlSystem.Server.Services.MediatR.Commands
{
    public sealed class SaveOrUpdateSensorsDataCommand : IRequest<bool>
    {
        public SensorsData SensorsData { get; init; }
    }
}
