using MonitoringSystem.Protos;
using Grpc.Core;
using Grpc.Net.Client;
using Microsoft.VisualBasic.FileIO;
using Client = MonitoringSystem.Protos.GrpcForecast.GrpcForecastClient;

namespace MonitoringSystem;

internal static class Program
{
    private static int _rawIndex;

    private static async Task Main()
    {
        var dataSetLocation = $"{Directory.GetCurrentDirectory()}\\Dataset with headers.csv";

        var dataSet = GetDataSet(dataSetLocation);

        var client = GetClient("https://localhost:7286");

        var sendingRequestTokenSource = new CancellationTokenSource();

        Console.WriteLine("!!!Sending requests has been started!!!");

        using (AsyncDuplexStreamingCall<GrpcForecastRequest, GrpcForecastReply> call = client.SendDataToPredictStream())
        {
            var getResponsesTask = GetResponsesFromStream(call);

            _ = SendRequestsOverStream(call, dataSet, sendingRequestTokenSource.Token);

            var command = Console.ReadLine();

            while (command != "stop")
            {
                command = Console.ReadLine();
            }

            sendingRequestTokenSource.Cancel();

            await call.RequestStream.CompleteAsync();

            await getResponsesTask;
        }

        Console.WriteLine("!!!Sending requests has been stopped!!!");

        Console.ReadLine();
    }

    private static async Task GetResponsesFromStream(AsyncDuplexStreamingCall<GrpcForecastRequest, GrpcForecastReply> call)
    {
        await foreach (var response in call.ResponseStream.ReadAllAsync())
        {
            Console.WriteLine(response);
        }
    }

    private static async Task SendRequestsOverStream(AsyncDuplexStreamingCall<GrpcForecastRequest, GrpcForecastReply> call,
        float[][] dataSet, CancellationToken token)
    {
        while (token.IsCancellationRequested is false)
        {
            await Task.Delay(5000, token);
            await call.RequestStream.WriteAsync(GenerateRequest(dataSet), token);
        }

        await Task.Delay(3000, token);

        await call.RequestStream.CompleteAsync();
    }

    private static GrpcForecastRequest GenerateRequest(float[][] dataSet)
    {
        var requestData = new float[12];

        for (int i = 0; i < requestData.Length; i++)
        {
            requestData[i] = dataSet[_rawIndex][i];
        }

        var request = new GrpcForecastRequest
        {
            ClusterLoad = requestData[0],
            CpuUsage = requestData[1],
            ClusterTemperature = requestData[2],
            Temperature = requestData[3],
            Humidity = requestData[4],
            AirHumidityOutside = requestData[5],
            AirDryTemperatureOutside = requestData[6],
            AirWetTemperatureOutside = requestData[7],
            WindSpeed = requestData[8],
            WindDirection = requestData[9],
            WindEnthalpy = requestData[10],
            CoolingValue = requestData[11]
        };

        _rawIndex++;

        if (_rawIndex == dataSet.GetLength(0) - 1)
        {
            _rawIndex = 0;
        }

        return request;
    }

    private static Client GetClient(string address)
    {
        var channel = GrpcChannel.ForAddress(address);

        return new Client(channel);
    }

    private static float[][] GetDataSet(string dataSetLocation)
    {
        using var parser = new TextFieldParser(dataSetLocation);
            
        var list = new List<List<float>>();

        parser.TextFieldType = FieldType.Delimited;
            
        parser.SetDelimiters(";");
            
        while (parser.EndOfData is false)
        {
            list.Add(new List<float>());
                
            foreach (var field in parser.ReadFields())
            {
                list.Last().Add(float.Parse(field.Replace('.', ',')));
            }
        }
            
        return list.Select(subList => subList.ToArray()).ToArray();
    }
}