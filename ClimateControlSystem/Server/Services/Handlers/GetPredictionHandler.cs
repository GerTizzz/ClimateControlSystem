using ClimateControlSystem.Server.Services.Queries;
using ClimateControlSystem.Shared;
using MediatR;

namespace ClimateControlSystem.Server.Services.Handlers
{
    public class GetPredictionHandler : IRequestHandler<GetPredictionQuery, PredictionResult>
    {
        private readonly IPredictionManager _predictionManager;

        public GetPredictionHandler(IPredictionManager predictionManager)
        {
            _predictionManager = predictionManager;
        }

        public async Task<PredictionResult> Handle(GetPredictionQuery request, CancellationToken cancellationToken)
        {
            return await Task.FromResult(_predictionManager.Predict(request.Data));
        }
    }
}
