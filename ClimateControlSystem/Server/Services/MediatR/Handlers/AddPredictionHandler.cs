using ClimateControlSystem.Server.Domain.Repositories;
using ClimateControlSystem.Server.Services.MediatR.Commands;
using MediatR;

namespace ClimateControlSystem.Server.Services.MediatR.Handlers
{
    public class AddPredictionHandler : IRequestHandler<AddClimateCommand, bool>
    {
        private readonly IMicroclimateRepository _predictionRepository;

        public AddPredictionHandler(IMicroclimateRepository predictionRepository)
        {
            _predictionRepository = predictionRepository;
        }

        public async Task<bool> Handle(AddClimateCommand request, CancellationToken cancellationToken)
        {
            return await _predictionRepository.AddMicroclimateAsync(request.Prediction, request.SensorData, request.TemperatureEvent, request.HumidityEvent);
        }
    }
}
