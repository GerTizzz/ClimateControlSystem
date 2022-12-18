using ClimateControlSystem.Server.Domain.Repositories;
using ClimateControlSystem.Server.Services.MediatR.Commands;
using MediatR;

namespace ClimateControlSystem.Server.Services.MediatR.Handlers
{
    public class AddAccuracyHandler : IRequestHandler<AddAccuracyCommand, bool>
    {
        private readonly IClimateRepository _predictionRepository;

        public AddAccuracyHandler(IClimateRepository predictionRepository)
        {
            _predictionRepository = predictionRepository;
        }

        public async Task<bool> Handle(AddAccuracyCommand request, CancellationToken cancellationToken)
        {
            return await _predictionRepository.AddAccuracyAsync(request.Accuracy);
        }
    }
}
