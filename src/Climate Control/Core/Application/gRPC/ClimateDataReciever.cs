using Grpc.Core;
using MediatR;
using Application.MediatR.ForecastService;
using Application.gRCP.Protos;

namespace Application.gRPC
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
                Label labelResult = await _mediatr.Send(new ProcessMicroclimateQuery(grpcRequest));
                reply.Reply = $"[Status: Success][Time: {DateTime.Now:HH:mm:ss dd:MM:yyyy}][Data: {labelResult.Temperature}, {labelResult.Humidity}]";
            }
            catch
            {
                reply.Reply = $"[Status: Failed][Time: {DateTime.Now:HH:mm:ss dd:MM:yyyy}][Data: {grpcRequest}]";
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
                        var predictionResult = await _mediatr.Send(new ProcessMicroclimateQuery(request));
                        reply.Reply = $"[Status: Success][Time: {DateTime.Now:HH:mm:ss dd:MM:yyyy}][Data: {predictionResult.Temperature}, {predictionResult.Humidity}]";
                    }
                    catch
                    {
                        reply.Reply = $"[Status: Failed][Time: {DateTime.Now:HH:mm:ss dd:MM:yyyy}][Data: {requestStream}]";
                    }

                    await responseStream.WriteAsync(reply);
                }
            }
            catch (Exception)
            {
                Console.WriteLine($"!!!!! I HAVE GOT EXCEPTION AT {nameof(SendDataToPredictStream)} METHOD !!!!!");
            }
        }
    }
}
