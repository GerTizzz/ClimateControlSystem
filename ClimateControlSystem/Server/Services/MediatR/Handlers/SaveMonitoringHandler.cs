using ClimateControlSystem.Server.Domain.Repositories;
using ClimateControlSystem.Server.Services.MediatR.Commands;
using MediatR;

namespace ClimateControlSystem.Server.Services.MediatR.Handlers
{
    public sealed class SaveMonitoringHandler : IRequestHandler<SaveMonitoringCommand, bool>
    {
        private readonly IMicroclimateRepository _predictionRepository;

        public SaveMonitoringHandler(IMicroclimateRepository predictionRepository)
        {
            _predictionRepository = predictionRepository;
        }

        public async Task<bool> Handle(SaveMonitoringCommand request, CancellationToken cancellationToken)
        {
            return await _predictionRepository.SaveMonitoringAsync(request.Monitoring);
        }
    }
}
