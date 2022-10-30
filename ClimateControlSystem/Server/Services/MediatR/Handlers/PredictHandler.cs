using ClimateControlSystem.Server.Domain.Services;
using ClimateControlSystem.Server.Resources.Common;
using ClimateControlSystem.Server.Services.Queries;
using MediatR;

namespace ClimateControlSystem.Server.Services.Handlers
{
    public class PredictHandler : IRequestHandler<PredictQuery, PredictionResult>
    {
        private readonly IPredictionService _predictionService;

        public PredictHandler(IPredictionService predictionService)
        {
            _predictionService = predictionService;
        }

        public async Task<PredictionResult> Handle(PredictQuery request, CancellationToken cancellationToken)
        {
            return await _predictionService.Predict(request.Data);
        }
    }
}
