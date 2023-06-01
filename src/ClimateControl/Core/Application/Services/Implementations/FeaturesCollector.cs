using Application.Primitives;
using Application.Services.Strategies;
using Domain.Repositories;

namespace Application.Services.Implementations
{
    public sealed class FeaturesCollector : IFeaturesCollector
    {
        public List<Feature> Features { get; private set; }

        private readonly IForecastsRepository _forecastsRepository;

        public bool IsEnoughData => Features.Count >= TensorPredictionRequest.NumberOfDataSets;

        public FeaturesCollector(IForecastsRepository forecastsRepository)
        {
            _forecastsRepository = forecastsRepository;
            Features = _forecastsRepository.GetLastFeatures(TensorPredictionRequest.NumberOfDataSets).Result.ToList();
        }

        public void AddNewData(Feature feature)
        {
            Features.Add(feature);

            RemoveExtraFeatures();
        }

        public void RemoveExtraFeatures()
        {
            while (Features.Count > TensorPredictionRequest.NumberOfDataSets)
            {
                Features.RemoveAt(0);
            }
        }
    }
}
