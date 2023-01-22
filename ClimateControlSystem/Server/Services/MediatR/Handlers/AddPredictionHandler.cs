using ClimateControlSystem.Server.Domain.Repositories;
using ClimateControlSystem.Server.Services.MediatR.Commands;
using MediatR;

namespace ClimateControlSystem.Server.Services.MediatR.Handlers
{
    public class AddPredictionHandler : IRequestHandler<AddPredictionCommand, bool>
    {
        private readonly IMicroclimateRepository _predictionRepository;

        public AddPredictionHandler(IMicroclimateRepository predictionRepository)
        {
            _predictionRepository = predictionRepository;
        }

        public async Task<bool> Handle(AddPredictionCommand request, CancellationToken cancellationToken)
        {
            return await _predictionRepository.AddPredictionAsync(request.Predicition, request.TemperatureEvent, request.HumidityEvent);
        }
    }
}
