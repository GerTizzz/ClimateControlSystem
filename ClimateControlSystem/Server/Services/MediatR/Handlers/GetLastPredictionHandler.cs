using ClimateControlSystem.Server.Domain.Repositories;
using ClimateControlSystem.Server.Resources.Common;
using ClimateControlSystem.Server.Services.MediatR.Queries;
using MediatR;

namespace ClimateControlSystem.Server.Services.MediatR.Handlers
{
    public class GetLastPredictionHandler : IRequestHandler<GetLastPredictionQuery, PredictionResultData>
    {
        private readonly IMicroclimateRepository _predictionRepository;

        public GetLastPredictionHandler(IMicroclimateRepository predictionRepository)
        {
            _predictionRepository = predictionRepository;
        }

        public async Task<PredictionResultData> Handle(GetLastPredictionQuery request, CancellationToken cancellationToken)
        {
            return await _predictionRepository.GetLastPredictionAsync();
        }
    }
}
