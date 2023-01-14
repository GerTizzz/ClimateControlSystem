using ClimateControlSystem.Server.Resources.Common;
using ClimateControlSystem.Shared;

namespace ClimateControlSystem.Server.Domain.Services
{
    public interface IPredictionEngineService
    {
        Task<PredictionResultData> Predict(SensorsData inputData);
    }
}
