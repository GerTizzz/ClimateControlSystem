using Domain.Entities;
using MediatR;

namespace WebApi.Services.MediatR.Queries.PredictionEngine
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
