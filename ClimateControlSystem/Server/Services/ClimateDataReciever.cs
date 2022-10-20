using ClimateControlSystem.Server.Protos;
using ClimateControlSystem.Server.Services.Queries;
using ClimateControlSystem.Shared;
using Grpc.Core;
using MediatR;

namespace ClimateControlSystem.Server.Services
{
    public sealed class ClimateDataReciever : ClimateMonitoring.ClimateMonitoringBase
    {
        private readonly IMediator _mediatr;

        public ClimateDataReciever(IMediator mediatr)
        {
            _mediatr = mediatr;
        }

        public override async Task<ClimateMonitoringReply> SendDataToPredict(ClimateMonitoringRequest grpcRequest, ServerCallContext context)
        {
            var reply = new ClimateMonitoringReply();

            try
            {
                PredictionRequest predictionRequest = ConvertGrpcRequstToPredictionRequest(grpcRequest);
                PredictionResult predictionResult = await _mediatr.Send(new GetPredictionQuery() { Data = predictionRequest });
                reply.Reply = $"[Status: Success][Time: {DateTime.Now.ToString("HH:mm:ss dd:MM:yyyy")}][Data: {predictionResult.PredictedTemperature}, {predictionResult.PreviousHumidity}]";
            }
            catch
            {
                reply.Reply = $"[Status: Failed][Time: {DateTime.Now.ToString("HH:mm:ss dd:MM:yyyy")}][Data: {grpcRequest.ToString()}]";
            }

            return await Task.FromResult(reply);
        }

        public override async Task SendDataToPredictStream(IAsyncStreamReader<ClimateMonitoringRequest> requestStream, IServerStreamWriter<ClimateMonitoringReply> responseStream, ServerCallContext context)
        {
            try
            {
                await foreach (var request in requestStream.ReadAllAsync())
                {
                    var reply = new ClimateMonitoringReply();

                    try
                    {
                        PredictionRequest predictionRequest = ConvertGrpcRequstToPredictionRequest(request);
                        PredictionResult predictionResult = await _mediatr.Send(new GetPredictionQuery() { Data = predictionRequest });
                        reply.Reply = $"[Status: Success][Time: {DateTime.Now.ToString("HH:mm:ss dd:MM:yyyy")}][Data: {predictionResult.PredictedTemperature}, {predictionResult.PreviousHumidity}]";
                    }
                    catch
                    {
                        reply.Reply = $"[Status: Failed][Time: {DateTime.Now.ToString("HH:mm:ss dd:MM:yyyy")}][Data: {requestStream.ToString()}]";
                    }

                    await responseStream.WriteAsync(reply);
                }
            }
            catch (Exception exc)
            {
                Console.WriteLine($"!!!!! I HAVE GOT EXCEPTION AT {nameof(SendDataToPredictStream)} METHOD !!!!!");
            }
        }

        private PredictionRequest ConvertGrpcRequstToPredictionRequest(ClimateMonitoringRequest grpcRequest)
        {
            PredictionRequest request = null;

            try
            {
                request = new PredictionRequest()
                {
                    ClusterLoad = grpcRequest.ClusterLoad,
                    CpuUsage = grpcRequest.CpuUsage,
                    ClusterTemperature = grpcRequest.ClusterTemperature,
                    PreviousTemperature = grpcRequest.PreviousTemperature,
                    PreviousHumidity = grpcRequest.PreviousHumidity,
                    AirHumidityOutside = grpcRequest.AirHumidityOutside,
                    AirDryTemperatureOutside = grpcRequest.AirDryTemperatureOutside,
                    AirWetTemperatureOutside = grpcRequest.AirWetTemperatureOutside,
                    WindSpeed = grpcRequest.WindSpeed,
                    WindDirection = grpcRequest.WindDirection,
                    WindEnthalpy = grpcRequest.WindEnthalpy,
                    MeanCoolingValue = grpcRequest.MeanCoolingValue
                };
            }
            catch (Exception exc)
            {
                Console.WriteLine($"!!!!! I HAVE GOT EXCEPTION AT {nameof(ConvertGrpcRequstToPredictionRequest)} METHOD !!!!!");
            }

            return request;
        }
    }
}
