using ClimateControlSystem.Server.Resources.Common;

namespace ClimateControlSystem.Server.Domain.Services
{
    public interface IPredictionService
    {
        public Task<PredictionResult> Predict(IncomingMonitoringData incomingRequest);
    }
}
