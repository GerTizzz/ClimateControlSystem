using ClimateControlSystem.Server.Resources.Domain;

namespace ClimateControlSystem.Server.Domain.Services
{
    public interface IMonitoringService
    {
        public Task<Prediction> Predict(FeaturesData featuresData);
    }
}
