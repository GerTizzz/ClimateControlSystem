using ClimateControl.Domain.Resources;

namespace ClimateControl.Domain.Services
{
    public interface IMonitoringService
    {
        public Task<Prediction> Predict(FeaturesData featuresData);
    }
}
