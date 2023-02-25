using AutoMapper;
using ClimateControlSystem.Server.Domain.Repositories;
using ClimateControlSystem.Server.Resources.Common;
using ClimateControlSystem.Server.Services.MediatR.Queries;
using ClimateControlSystem.Server.Services.MediatR.Queries.MicroclimateRepository;
using MediatR;

namespace ClimateControlSystem.Server.Services.MediatR.Handlers.MicroclimateRepository
{
    public sealed class TryGetLastPredictionHandler : IRequestHandler<TryGetLastPredictionQuery, Prediction?>
    {
        private readonly IMicroclimateRepository _predictionRepository;
        private readonly IMapper _mapper;

        public TryGetLastPredictionHandler(IMicroclimateRepository predictionRepository, IMapper mapper)
        {
            _predictionRepository = predictionRepository;
            _mapper = mapper;
        }

        public async Task<Prediction?> Handle(TryGetLastPredictionQuery request, CancellationToken cancellationToken)
        {
            var predictionRecord = await _predictionRepository.TryGetLastPredictionAsync();

            var prediction = _mapper.Map<Prediction>(predictionRecord);

            return prediction;
        }
    }
}
