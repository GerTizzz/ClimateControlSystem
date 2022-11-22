using ClimateControlSystem.Server.Domain.Repositories;
using ClimateControlSystem.Server.Services.MediatR.Commands;
using MediatR;

namespace ClimateControlSystem.Server.Services.MediatR.Handlers
{
    public class UpdatePredictionAccuraciesHandler : IRequestHandler<UpdatePredictionAccuraciesCommand, bool>
    {
        private readonly IMonitoringDataRepository _predictionRepository;

        public UpdatePredictionAccuraciesHandler(IMonitoringDataRepository predictionRepository)
        {
            _predictionRepository = predictionRepository;
        }

        public Task<bool> Handle(UpdatePredictionAccuraciesCommand request, CancellationToken cancellationToken)
        {
            return _predictionRepository.UpdatePredictionAccuracies(request.Data);
        }
    }
}
