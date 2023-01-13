using AutoMapper;
using ClimateControlSystem.Server.Protos;
using ClimateControlSystem.Server.Resources.Common;
using ClimateControlSystem.Server.Services.Queries;
using Grpc.Core;
using MediatR;

namespace ClimateControlSystem.Server.Services.gRPC
{
    public sealed class ClimateDataReciever : ClimateMonitoring.ClimateMonitoringBase
    {
        private readonly IMediator _mediatr;
        private readonly IMapper _mapper;

        public ClimateDataReciever(IMediator mediatr, IMapper mapper)
        {
            _mediatr = mediatr;
            _mapper = mapper;
        }

        public override async Task<ClimateMonitoringReply> SendDataToPredict(ClimateMonitoringRequest grpcRequest, ServerCallContext context)
        {
            var reply = new ClimateMonitoringReply();
            
            try
            {
                SensorsData predictionRequest = _mapper.Map<SensorsData>(grpcRequest);
                PredictionResultData predictionResult = await _mediatr.Send(new PredictQuery() { Data = predictionRequest });
                reply.Reply = $"[Status: Success][Time: {DateTime.Now.ToString("HH:mm:ss dd:MM:yyyy")}][Data: {predictionResult.PredictedTemperature}, {predictionResult.PredictedHumidity}]";
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
                        SensorsData predictionRequest = _mapper.Map<SensorsData>(request);
                        PredictionResultData predictionResult = await _mediatr.Send(new PredictQuery() { Data = predictionRequest });
                        reply.Reply = $"[Status: Success][Time: {DateTime.Now.ToString("HH:mm:ss dd:MM:yyyy")}][Data: {predictionResult.PredictedTemperature}, {predictionResult.PredictedHumidity}]";
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
    }
}
