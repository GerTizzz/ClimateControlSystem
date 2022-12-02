using ClimateControlSystem.Server.Domain.Repositories;
using ClimateControlSystem.Server.Services.MediatR.Queries;
using ClimateControlSystem.Shared;
using MediatR;

namespace ClimateControlSystem.Server.Services.MediatR.Handlers
{
    public class GetLastPredictionHandler : IRequestHandler<GetLastPredictionQuery, PredictionData>
    {
        private readonly IClimateRepository _predictionRepository;

        public GetLastPredictionHandler(IClimateRepository predictionRepository)
        {
            _predictionRepository = predictionRepository;
        }

        public async Task<PredictionData> Handle(GetLastPredictionQuery request, CancellationToken cancellationToken)
        {
            return await _predictionRepository.GetLastPredictionAsync();
        }
    }
}
