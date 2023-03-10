using AutoMapper;
using MediatR;
using WebApi.Infrastructure.Repositories;
using WebApi.Resources.Repository.TablesEntities;
using WebApi.Services.MediatR.Commands.MonitoringsRepository;

namespace WebApi.Services.MediatR.Handlers.MonitoringsRepository
{
    public sealed class SaveMonitoringHandler : IRequestHandler<SaveMonitoringCommand, bool>
    {
        private readonly IMonitoringsRepository _predictionRepository;
        private readonly IMapper _mapper;

        public SaveMonitoringHandler(IMonitoringsRepository predictionRepository, IMapper mapper)
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
