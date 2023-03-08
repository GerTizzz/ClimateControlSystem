using MediatR;

namespace ClimateControl.Server.Services.MediatR.Queries.PredictionEngine
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
