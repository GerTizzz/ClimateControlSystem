using Grpc.Net.Client;
using ClimateMonitoringSystem.Protos;
using Microsoft.VisualBasic.FileIO;
using Client = ClimateMonitoringSystem.Protos.ClimateMonitoring.ClimateMonitoringClient;

namespace ClimateMonitoringSystem
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            string dataSetLocation = Directory.GetCurrentDirectory() + "\Dataset with headers.csv";

            float[][] dataSet = GetDataSet(dataSetLocation);

            var client = GetClient("https://localhost:7286");

            CancellationTokenSource tokenSource = new CancellationTokenSource();

            SendRequest(client, dataSet, tokenSource).Start();

            Console.WriteLine("ya tut");
        }

        static Client GetClient(string address)
        {
            var channel = GrpcChannel.ForAddress(address);

            return new Client(channel);
        }

        static async Task SendRequest(Client client, float[][] dataSet, CancellationTokenSource tokenSource)
        {
            while (tokenSource.IsCancellationRequested is false)
            {
                await Task.Delay(15000);
                var response = await client.PredictAsync(GenerateRequest(dataSet));
                Console.WriteLine(response);
            }
        }

        static ClimateMonitoringRequest GenerateRequest(float[][] dataSet)
        {
            Random random = new Random();

            float[] requestData = new float[12];

            for (int i = 0; i < requestData.Length; i++)
            {
                int arrayIndex = random.Next(0, dataSet.Length);
                requestData[i] = dataSet[arrayIndex][i];
            }

            return new ClimateMonitoringRequest()
            {
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
        }

        static float[][] GetDataSet(string dataSetLocation)
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
                        list.Last().Add(float.Parse(field));
                    }
                }

                dataSet = list.Select(subList => subList.ToArray()).ToArray();
            }

            return dataSet;
        }
    }
}