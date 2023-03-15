using AutoMapper;
using Domain.Repositories;
using MediatR;

namespace Application.MediatR.ForecastRepository
{
    public sealed class TryGetLastPredictionHandler : IRequestHandler<TryGetLastPredictionQuery, Label?>
    {
        private readonly IForecastRepository _predictionRepository;
        private readonly IMapper _mapper;

        public TryGetLastPredictionHandler(IForecastRepository predictionRepository, IMapper mapper)
        {
            _predictionRepository = predictionRepository;
            _mapper = mapper;
        }

        public async Task<Label?> Handle(TryGetLastPredictionQuery request, CancellationToken cancellationToken)
        {
            var predictionRecord = await _predictionRepository.TryGetLastPredictionAsync();

            var prediction = _mapper.Map<Label>(predictionRecord);

            return prediction;
        }
    }
}
