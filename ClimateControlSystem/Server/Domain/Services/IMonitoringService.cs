using ClimateControlSystem.Server.Resources.Common;

namespace ClimateControlSystem.Server.Domain.Services
{
    public interface IMonitoringService
    {
        public Task<PredictionResult> Predict(SensorsData incomingRequest);
    }
}
