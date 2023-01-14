using ClimateControlSystem.Server.Resources.Common;
using ClimateControlSystem.Shared;

namespace ClimateControlSystem.Server.Domain.Services
{
    public interface IPredictionService
    {
        public Task<PredictionResultData> Predict(SensorsData incomingRequest);
    }
}
