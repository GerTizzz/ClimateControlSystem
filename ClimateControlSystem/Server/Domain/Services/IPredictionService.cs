using ClimateControlSystem.Shared;

namespace ClimateControlSystem.Server.Domain.Services
{
    public interface IPredictionService
    {
        public Task<PredictionData> Predict(MonitoringData incomingRequest);
    }
}
