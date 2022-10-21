using AutoMapper;
using ClimateControlSystem.Server.Domain.Services;
using ClimateControlSystem.Server.Resources;
using ClimateControlSystem.Server.Services.MediatR.Commands;
using MediatR;

namespace ClimateControlSystem.Server.Services
{
    public class PredictionService : IPredictionService
    {
        private readonly IPredictionEngineService _predictionEngine;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public PredictionService(IPredictionEngineService predictionManager, IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
            _predictionEngine = predictionManager;
        }

        public async Task<PredictionResult> GetPrediction(PredictionRequest incomingRequest)
        {
            PredictionResult predictionResult =  _predictionEngine.Predict(incomingRequest);

            ClimateRecord newClimateRecord = CreateClimateRecord(incomingRequest, predictionResult);

            _ = _mediator.Send(new SavePredictionCommand() { Data = newClimateRecord });

            return predictionResult;
        }

        private ClimateRecord CreateClimateRecord(PredictionRequest incomingRequest, PredictionResult predictionResult)
        {
            ClimateRecord newClimateRecord = _mapper.Map<ClimateRecord>(incomingRequest);

            newClimateRecord.PredictedTemperature = predictionResult.PredictedTemperature;
            newClimateRecord.PredictedHumidity = predictionResult.PredictedHumidity;

            return newClimateRecord;
        }
    }
}
