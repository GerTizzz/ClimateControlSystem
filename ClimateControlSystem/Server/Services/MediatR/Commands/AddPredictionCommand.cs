using ClimateControlSystem.Server.Resources.Common;
using ClimateControlSystem.Shared.Enums;
using MediatR;

namespace ClimateControlSystem.Server.Services.MediatR.Commands
{
    public class AddPredictionCommand : IRequest<bool>
    {
        public PredictionResult Prediction { get; init; }
        public AccuracyData Accuracy { get; init; }
        public MonitoringData Monitoring { get; init; }
        public ClimateEventType ClimateEventType { get; init; }
    }
}
