using MediatR;
using Application.MediatR.ForecastService;
using Application.gRCP.Protos;
using Grpc.Core;

namespace Application.gRPC
{
    public sealed class ClimateDataReciever : GrpcForecast.GrpcForecastBase
    {
        private readonly IMediator _mediatr;

        public ClimateDataReciever(IMediator mediatr)
        {
            _mediatr = mediatr;
        }

        public override async Task SendDataToPredictStream(IAsyncStreamReader<GrpcForecastRequest> requestStream, IServerStreamWriter<GrpcForecastReply> responseStream, ServerCallContext context)
        {
            try
            {
                await foreach (var request in requestStream.ReadAllAsync())
                {
                    var reply = new GrpcForecastReply();

                    try
                    {
                        var predictionResult = await _mediatr.Send(new ProcessMicroclimateQuery(request));
                        reply.Reply = $"[Status: Success][Time: {DateTime.Now:HH:mm:ss dd:MM:yyyy}][Data: {predictionResult.Temperature}, {predictionResult.Humidity}]";
                    }
                    catch (Exception e)
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
