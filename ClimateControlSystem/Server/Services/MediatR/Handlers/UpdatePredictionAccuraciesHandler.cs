using ClimateControlSystem.Server.Domain.Repositories;
using ClimateControlSystem.Server.Services.MediatR.Commands;
using MediatR;

namespace ClimateControlSystem.Server.Services.MediatR.Handlers
{
    public class UpdatePredictionAccuraciesHandler : IRequestHandler<UpdatePredictionAccuraciesCommand, bool>
    {
        private readonly IPredictionRepository _predictionRepository;

        public UpdatePredictionAccuraciesHandler(IPredictionRepository predictionRepository)
        {
            _predictionRepository = predictionRepository;
        }

        public Task<bool> Handle(UpdatePredictionAccuraciesCommand request, CancellationToken cancellationToken)
        {
            return _predictionRepository.UpdatePredictionAccuracies(request.Data);
        }
    }
}
