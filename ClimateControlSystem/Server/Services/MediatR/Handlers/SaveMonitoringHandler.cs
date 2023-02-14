using AutoMapper;
using ClimateControlSystem.Server.Domain.Repositories;
using ClimateControlSystem.Server.Resources.Repository.TablesEntities;
using ClimateControlSystem.Server.Services.MediatR.Commands;
using MediatR;

namespace ClimateControlSystem.Server.Services.MediatR.Handlers
{
    public sealed class SaveMonitoringHandler : IRequestHandler<SaveMonitoringCommand, bool>
    {
        private readonly IMicroclimateRepository _predictionRepository;
        private readonly IMapper _mapper;

        public SaveMonitoringHandler(IMicroclimateRepository predictionRepository, IMapper mapper)
        {
            _predictionRepository = predictionRepository;
            _mapper = mapper;
        }

        public async Task<bool> Handle(SaveMonitoringCommand request, CancellationToken cancellationToken)
        {
            var monitroingRecord = _mapper.Map<MonitoringsEntity>(request.Monitoring);

            return await _predictionRepository.SaveMonitoringAsync(monitroingRecord);
        }
    }
}
