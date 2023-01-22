using ClimateControlSystem.Server.Domain.Repositories;
using ClimateControlSystem.Server.Services.MediatR.Commands;
using MediatR;

namespace ClimateControlSystem.Server.Services.MediatR.Handlers
{
    public class AddSensorsDataHandler : IRequestHandler<AddSensorsDataCommand, bool>
    {
        private readonly IMicroclimateRepository _predictionRepository;

        public AddSensorsDataHandler(IMicroclimateRepository predictionRepository)
        {
            _predictionRepository = predictionRepository;
        }

        public async Task<bool> Handle(AddSensorsDataCommand request, CancellationToken cancellationToken)
        {
            return await _predictionRepository.AddSensorsDataAsync(request.SensorData);
        }
    }
}
