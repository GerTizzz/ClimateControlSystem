using AutoMapper;
using ClimateControlSystem.Server.Domain.Repositories;
using ClimateControlSystem.Server.Resources.Common;
using ClimateControlSystem.Server.Services.MediatR.Queries;
using MediatR;

namespace ClimateControlSystem.Server.Services.MediatR.Handlers
{
    public sealed class TryGetLastActualDataHandler : IRequestHandler<TryGetLastActualDataQuery, ActualData>
    {
        private readonly IMicroclimateRepository _predictionRepository;
        private readonly IMapper _mapper;

        public TryGetLastActualDataHandler(IMicroclimateRepository predictionRepository, IMapper mapper)
        {
            _predictionRepository = predictionRepository;
            _mapper = mapper;
        }

        public async Task<ActualData> Handle(TryGetLastActualDataQuery request, CancellationToken cancellationToken)
        {
            var actualDataRecord = await _predictionRepository.TryGetLastActualDataAsync();

            var actualData = _mapper.Map<ActualData>(actualDataRecord);

            return actualData;
        }
    }
}
