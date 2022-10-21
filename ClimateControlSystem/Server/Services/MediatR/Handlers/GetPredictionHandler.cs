using ClimateControlSystem.Server.Domain.Services;
using ClimateControlSystem.Server.Resources;
using ClimateControlSystem.Server.Services.Queries;
using MediatR;

namespace ClimateControlSystem.Server.Services.Handlers
{
    public class GetPredictionHandler : IRequestHandler<GetPredictionQuery, PredictionResult>
    {
        private readonly IPredictionService _predictionService;

        public GetPredictionHandler(IPredictionService predictionService)
        {
            _predictionService = predictionService;
        }

        public async Task<PredictionResult> Handle(GetPredictionQuery request, CancellationToken cancellationToken)
        {
            return await _predictionService.GetPrediction(request.Data);
        }
    }
}
