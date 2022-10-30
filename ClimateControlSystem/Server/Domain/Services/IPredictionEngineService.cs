using ClimateControlSystem.Server.Resources.Common;

namespace ClimateControlSystem.Server.Domain.Services
{
    public interface IPredictionEngineService
    {
        PredictionResult Predict(IncomingMonitoringData inputData);
    }
}
