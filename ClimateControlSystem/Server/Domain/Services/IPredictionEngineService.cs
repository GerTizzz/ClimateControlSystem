using ClimateControlSystem.Shared;

namespace ClimateControlSystem.Server.Domain.Services
{
    public interface IPredictionEngineService
    {
        PredictionData Predict(MonitoringData inputData);
    }
}
