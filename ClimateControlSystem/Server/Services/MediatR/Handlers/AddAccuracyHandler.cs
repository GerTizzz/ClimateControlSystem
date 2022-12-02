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

        public Task<bool> Handle(AddAccuracyCommand request, CancellationToken cancellationToken)
        {
            return _predictionRepository.AddAccuracyAsync(request.Data);
        }
    }
}
