using ClimateControlSystem.Server.Domain.Services;
using ClimateControlSystem.Server.Services.Queries;
using ClimateControlSystem.Shared;
using MediatR;

namespace ClimateControlSystem.Server.Services.Handlers
{
    public class PredictHandler : IRequestHandler<PredictQuery, PredictionData>
    {
        private readonly IPredictionService _predictionService;

        public PredictHandler(IPredictionService predictionService)
        {
            _predictionService = predictionService;
        }

        public async Task<PredictionData> Handle(PredictQuery request, CancellationToken cancellationToken)
        {
            return await _predictionService.Predict(request.Data);
        }
    }
}
