using ClimateControlSystem.Server.Protos;
using Grpc.Core;

namespace ClimateControlSystem.Server.Services
{
    public sealed class ClimateMonitoringService : ClimateMonitoring.ClimateMonitoringBase
    {
        private readonly IPredictionService _predictorService;

        public ClimateMonitoringService(IPredictionService predictionService)
        {
            _predictorService = predictionService;
        }

        public override async Task<ClimateMonitoringReply> Predict(ClimateMonitoringRequest request, ServerCallContext context)
        {
            var reply = new ClimateMonitoringReply();

            try
            {
                float[] features = ConvertRequstToFloatArray(request);
                float[] prediction = _predictorService.Predict(features);
                reply.Reply = $"[Status: Success][Time: {DateTime.Now.ToString("HH:mm:ss dd:MM:yyyy")}][Data: {string.Join(' ', features)}]";
                // To do
            }
            catch
            {
                reply.Reply = $"[Status: Failed][Time: {DateTime.Now.ToString("HH:mm:ss dd:MM:yyyy")}][Data: {request.ToString()}]";
            }

            return await Task.FromResult(reply);
        }

        private float[] ConvertRequstToFloatArray(ClimateMonitoringRequest request)
        {
            float[] features = new float[12];

            try
            {
                features[0] = request.ClusterLoad;
                features[1] = request.CpuUsage;
                features[2] = request.ClusterTemperature;
                features[3] = request.PreviousTemperature;
                features[4] = request.PreviousHumidity;
                features[5] = request.AirHumidityOutside;
                features[6] = request.AirDryTemperatureOutside;
                features[7] = request.AirWetTemperatureOutside;
                features[8] = request.WindSpeed;
                features[9] = request.WindDirection;
                features[10] = request.WindEnthalpy;
                features[11] = request.MeanCoolingValue;
            }
            catch
            {
                throw new ArgumentException(nameof(request));
            }

            return features;
        }
    }
}
