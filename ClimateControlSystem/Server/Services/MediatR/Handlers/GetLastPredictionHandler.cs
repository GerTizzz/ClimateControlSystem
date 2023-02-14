using AutoMapper;
using ClimateControlSystem.Server.Domain.Repositories;
using ClimateControlSystem.Server.Resources.Common;
using ClimateControlSystem.Server.Services.MediatR.Queries;
using MediatR;

namespace ClimateControlSystem.Server.Services.MediatR.Handlers
{
    public sealed class GetLastPredictionHandler : IRequestHandler<GetLastPredictionQuery, Prediction>
    {
        private readonly IMicroclimateRepository _predictionRepository;
        private readonly IMapper _mapper;

        public GetLastPredictionHandler(IMicroclimateRepository predictionRepository, IMapper mapper)
        {
            _predictionRepository = predictionRepository;
            _mapper = mapper;
        }

        public async Task<Prediction> Handle(GetLastPredictionQuery request, CancellationToken cancellationToken)
        {
            var predictionRecord = await _predictionRepository.GetLastPredictionAsync();

            var prediction = _mapper.Map<Prediction>(predictionRecord);

            return prediction;
        }
    }
}
