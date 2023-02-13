using ClimateControlSystem.Server.Domain.Repositories;
using ClimateControlSystem.Server.Services.MediatR.Commands;
using MediatR;

namespace ClimateControlSystem.Server.Services.MediatR.Handlers
{
    public sealed class SaveOrUpdateSensorsDataHandler : IRequestHandler<SaveOrUpdateSensorsDataCommand, bool>
    {
        private readonly IMicroclimateRepository _predictionRepository;

        public SaveOrUpdateSensorsDataHandler(IMicroclimateRepository predictionRepository)
        {
            _predictionRepository = predictionRepository;
        }

        public async Task<bool> Handle(SaveOrUpdateSensorsDataCommand request, CancellationToken cancellationToken)
        {
            return await _predictionRepository.SaveOrUpdateSensorsDataAsync(request.SensorsData);
        }
    }
}
