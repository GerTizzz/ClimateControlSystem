using Domain.Entities;

namespace Domain.Services
{
    public interface IMonitoringService
    {
        public Task<Prediction> Predict(FeaturesData featuresData);
    }
}
