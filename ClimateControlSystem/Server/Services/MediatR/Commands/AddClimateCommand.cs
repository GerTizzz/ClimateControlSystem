using ClimateControlSystem.Server.Resources.Common;
using ClimateControlSystem.Shared.Common;
using ClimateControlSystem.Shared.Enums;
using MediatR;

namespace ClimateControlSystem.Server.Services.MediatR.Commands
{
    public class AddClimateCommand : IRequest<bool>
    {
        public PredictionResult Prediction { get; init; }
        public MonitoringData Monitoring { get; init; }
        public List<ClimateEventType> ClimateEventType { get; init; }
        public Config Config { get; init; }
    }
}
