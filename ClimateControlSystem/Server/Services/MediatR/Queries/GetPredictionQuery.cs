using ClimateControlSystem.Server.Resources.Common;
using MediatR;

namespace ClimateControlSystem.Server.Services.MediatR.Queries
{
    public sealed class GetPredictionQuery : IRequest<Prediction>
    {
        public FeaturesData FeaturesData { get; }

        public GetPredictionQuery(FeaturesData featuresData)
        {
            FeaturesData = featuresData;
        }
    }
}
