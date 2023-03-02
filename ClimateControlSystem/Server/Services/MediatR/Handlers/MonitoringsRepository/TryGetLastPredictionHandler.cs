using AutoMapper;
using ClimateControlSystem.Server.Domain.Repositories;
using ClimateControlSystem.Server.Resources.Domain;
using ClimateControlSystem.Server.Services.MediatR.Queries.MonitoringsRepository;
using MediatR;

namespace ClimateControlSystem.Server.Services.MediatR.Handlers.MonitoringsRepository
{
    public sealed class TryGetLastPredictionHandler : IRequestHandler<TryGetLastPredictionQuery, Prediction?>
    {
        private readonly IMonitoringsRepository _predictionRepository;
        private readonly IMapper _mapper;

        public TryGetLastPredictionHandler(IMonitoringsRepository predictionRepository, IMapper mapper)
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
