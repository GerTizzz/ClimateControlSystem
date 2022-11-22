using ClimateControlSystem.Server.Domain.Repositories;
using ClimateControlSystem.Server.Services.MediatR.Queries;
using ClimateControlSystem.Shared;
using MediatR;

namespace ClimateControlSystem.Server.Services.MediatR.Handlers
{
    public class GetLastPredictionHandler : IRequestHandler<GetLastPredictionQuery, MonitoringData>
    {
        private readonly IMonitoringDataRepository _predictionRepository;

        public GetLastPredictionHandler(IMonitoringDataRepository predictionRepository)
        {
            _predictionRepository = predictionRepository;
        }

        public async Task<MonitoringData> Handle(GetLastPredictionQuery request, CancellationToken cancellationToken)
        {
            return await _predictionRepository.GetLastPredictionAsync();
        }
    }
}
