using ClimateControlSystem.Server.Resources.Common;

namespace ClimateControlSystem.Server.Domain.Services
{
    public interface IMonitoringService
    {
        public Task<Prediction> Predict(FeaturesData featuresData);
    }
}
