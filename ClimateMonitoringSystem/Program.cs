using Grpc.Net.Client;
using ClimateMonitoringSystem.Protos;
using Microsoft.VisualBasic.FileIO;
using Client = ClimateMonitoringSystem.Protos.ClimateMonitoring.ClimateMonitoringClient;
using Grpc.Core;

namespace ClimateMonitoringSystem
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            string dataSetLocation = Directory.GetCurrentDirectory() + "\\Dataset with headers.csv";

            float[][] dataSet = GetDataSet(dataSetLocation);

            var client = GetClient("https://localhost:7286");

            CancellationTokenSource sendingRequestTokenSource = new CancellationTokenSource();

            Console.WriteLine("!!!Sending requests has been started!!!");

            using (AsyncDuplexStreamingCall<ClimateMonitoringRequest, ClimateMonitoringReply> call = client.SendDataToPredictStream())
            {
                var getResponsesTask = GetResponsesFromStream(call);

                SendRequestsOverStream(call, dataSet, sendingRequestTokenSource);

                string command = Console.ReadLine();

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

        private static async Task GetResponsesFromStream(AsyncDuplexStreamingCall<ClimateMonitoringRequest, ClimateMonitoringReply> call)
        {
            await foreach (var response in call.ResponseStream.ReadAllAsync())
            {
                Console.WriteLine(response);
            }
        }

        private static async Task SendRequestsOverStream(AsyncDuplexStreamingCall<ClimateMonitoringRequest, ClimateMonitoringReply> call,
            float[][] dataSet, CancellationTokenSource tokenSource)
        {
            while (tokenSource.IsCancellationRequested is false)
            {
                await Task.Delay(5000);
                await call.RequestStream.WriteAsync(GenerateRequest(dataSet));                    
            }

            await call.RequestStream.CompleteAsync();
        }

        private static ClimateMonitoringRequest GenerateRequest(float[][] dataSet)
        {
            Random random = new Random();

            float[] requestData = new float[12];

            for (int i = 0; i < requestData.Length; i++)
            {
                int arrayIndex = random.Next(0, dataSet.Length);
                requestData[i] = dataSet[arrayIndex][i];
            }

            ClimateMonitoringRequest reqest = new ClimateMonitoringRequest()
            {
                ArrivedTimeTicks = DateTime.UtcNow.Ticks,
                ClusterLoad = requestData[0],
                CpuUsage = requestData[1],
                ClusterTemperature = requestData[2],
                PreviousTemperature = requestData[3],
                PreviousHumidity = requestData[4],
                AirHumidityOutside = requestData[5],
                AirDryTemperatureOutside = requestData[6],
                AirWetTemperatureOutside = requestData[7],
                WindSpeed = requestData[8],
                WindDirection = requestData[9],
                WindEnthalpy = requestData[10],
                MeanCoolingValue = requestData[11]
            };

            return reqest;
        }

        private static Client GetClient(string address)
        {
            var channel = GrpcChannel.ForAddress(address);

            return new Client(channel);
        }

        private static float[][] GetDataSet(string dataSetLocation)
        {
            float[][] dataSet;

            using (TextFieldParser parser = new TextFieldParser(dataSetLocation))
            {
                List<List<float>> list = new List<List<float>>();

                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(";");
                while (parser.EndOfData is false)
                {
                    list.Add(new List<float>());
                    string[] fields = parser.ReadFields();
                    foreach (string field in fields)
                    {
                        list.Last().Add(float.Parse(field.Replace('.', ',')));
                    }
                }

                dataSet = list.Select(subList => subList.ToArray()).ToArray();
            }

            return dataSet;
        }
    }
}