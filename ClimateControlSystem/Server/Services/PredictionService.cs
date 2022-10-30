using AutoMapper;
using ClimateControlSystem.Server.Domain.Services;
using ClimateControlSystem.Server.Resources.Common;
using ClimateControlSystem.Server.Services.MediatR.Commands;
using ClimateControlSystem.Server.Services.MediatR.Queries;
using ClimateControlSystem.Shared;
using MediatR;

namespace ClimateControlSystem.Server.Services
{
    public class PredictionService : IPredictionService
    {
        private readonly IPredictionEngineService _predictionEngine;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public PredictionService(IPredictionEngineService predictionEngine, IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
            _predictionEngine = predictionEngine;
        }

        public Task<PredictionResult> Predict(IncomingMonitoringData newClimateData)
        {
            PredictionResult predictionResult = _predictionEngine.Predict(newClimateData);

            _ = CreateClimateRecord(newClimateData, predictionResult);

            return Task.FromResult(predictionResult);
        }

        private Task CreateClimateRecord(IncomingMonitoringData incomingData, PredictionResult predictedData)
        {
            MonitoringData monitoringData = _mapper.Map<MonitoringData>(incomingData);

            CalculateLastPredictionAccuracy(monitoringData).Wait();

            monitoringData.PredictedTemperature = predictedData.PredictedTemperature;
            monitoringData.PredictedHumidity = predictedData.PredictedHumidity;

            _mediator.Send(new SavePredictionCommand() { Data = monitoringData });

             return Task.CompletedTask;
        }

        private Task CalculateLastPredictionAccuracy(MonitoringData monitoringData)
        {
            var actualTemperature = monitoringData.PreviousTemperature;
            var actualHumidity = monitoringData.PreviousHumidity;

            var lastRecord = _mediator.Send(new GetLastPredictionQuery()).Result;

            var predictedTemperature = lastRecord.PredictedTemperature;
            var predictedHumidity = lastRecord.PredictedHumidity;

            lastRecord.PredictedTemperatureAccuracy = 100f - Math.Abs(predictedTemperature - actualTemperature) / actualTemperature;
            lastRecord.PredictedHumidityAccuracy =  100f - Math.Abs(predictedHumidity - actualHumidity) / actualHumidity;

            _mediator.Send(new UpdatePredictionAccuraciesCommand() { Data = lastRecord }).Wait();

            return Task.CompletedTask;
        }
    }
}
