using ClimateControlSystem.Server.Domain.Repositories;
using ClimateControlSystem.Server.Services.MediatR.Commands;
using MediatR;

namespace ClimateControlSystem.Server.Services.MediatR.Handlers
{
    public class SavePredictionHandler : IRequestHandler<SavePredictionCommand, bool>
    {
        private readonly IMonitoringDataRepository _predictionRepository;

        public SavePredictionHandler(IMonitoringDataRepository predictionRepository)
        {
            _predictionRepository = predictionRepository;
        }

        public async Task<bool> Handle(SavePredictionCommand request, CancellationToken cancellationToken)
        {
            return await _predictionRepository.AddPredictionAsync(request.Data);
        }
    }
}
