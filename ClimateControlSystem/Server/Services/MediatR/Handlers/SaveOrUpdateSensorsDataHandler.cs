using AutoMapper;
using ClimateControlSystem.Server.Domain.Repositories;
using ClimateControlSystem.Server.Resources.Repository.TablesEntities;
using ClimateControlSystem.Server.Services.MediatR.Commands;
using MediatR;

namespace ClimateControlSystem.Server.Services.MediatR.Handlers
{
    public sealed class SaveSensorsDataHandler : IRequestHandler<SaveSensorsDataCommand, bool>
    {
        private readonly IMicroclimateRepository _predictionRepository;
        private readonly IMapper _mapper;

        public SaveSensorsDataHandler(IMicroclimateRepository predictionRepository, IMapper mapper)
        {
            _predictionRepository = predictionRepository;
            _mapper = mapper;
        }

        public async Task<bool> Handle(SaveSensorsDataCommand request, CancellationToken cancellationToken)
        {
            var sensorsDataRecord = _mapper.Map<SensorsDataEntity>(request.SensorsData);

            return await _predictionRepository.SaveSensorsDataAsync(sensorsDataRecord);
        }
    }
}
