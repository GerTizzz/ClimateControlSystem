using ClimateControlSystem.Server.Domain.Repositories;
using ClimateControlSystem.Server.Services.MediatR.Commands;
using MediatR;

namespace ClimateControlSystem.Server.Services.MediatR.Handlers
{
    public class SavePredictionHandler : IRequestHandler<SavePredictionCommand, int>
    {
        private readonly IPredictionRepository _predictionRepository;

        public SavePredictionHandler(IPredictionRepository predictionRepository)
        {
            _predictionRepository = predictionRepository;
        }

        public async Task<int> Handle(SavePredictionCommand request, CancellationToken cancellationToken)
        {
            return await _predictionRepository.AddPredictionAsync(request.Data);
        }
    }
}
