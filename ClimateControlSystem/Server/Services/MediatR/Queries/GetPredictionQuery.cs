using ClimateControlSystem.Server.Resources.Common;
using MediatR;

namespace ClimateControlSystem.Server.Services.MediatR.Queries
{
    public sealed class GetPredictionQuery : IRequest<Prediction>
    {
        public SensorsData sensorsData { get; }

        public GetPredictionQuery(SensorsData sensorsData)
        {
            this.sensorsData = sensorsData;
        }
    }
}
