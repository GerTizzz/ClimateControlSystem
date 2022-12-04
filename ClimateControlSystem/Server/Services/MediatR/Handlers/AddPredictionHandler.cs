using ClimateControlSystem.Server.Domain.Repositories;
using ClimateControlSystem.Server.Services.MediatR.Commands;
using MediatR;

namespace ClimateControlSystem.Server.Services.MediatR.Handlers
{
    public class AddPredictionHandler : IRequestHandler<AddPredictionCommand, bool>
    {
        private readonly IClimateRepository _predictionRepository;

        public AddPredictionHandler(IClimateRepository predictionRepository)
        {
            _predictionRepository = predictionRepository;
        }

        public async Task<bool> Handle(AddPredictionCommand request, CancellationToken cancellationToken)
        {
            return await _predictionRepository.AddPredictionAsync(request.Prediction, request.Monitoring, request.Accuracy, request.ClimateEventType);
        }
    }
}
