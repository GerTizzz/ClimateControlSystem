using ClimateControlSystem.Server.Domain.Services;
using ClimateControlSystem.Server.Resources.Common;
using ClimateControlSystem.Server.Services.Queries;
using MediatR;

namespace ClimateControlSystem.Server.Services.Handlers
{
    public sealed class PredictHandler : IRequestHandler<PredictQuery, Prediction>
    {
        private readonly IMonitoringService _predictionService;

        public PredictHandler(IMonitoringService predictionService)
        {
            _predictionService = predictionService;
        }

        public async Task<Prediction> Handle(PredictQuery request, CancellationToken cancellationToken)
        {
            return await _predictionService.Predict(request.Data);
        }
    }
}
