using AutoMapper;
using ClimateControlSystem.Server.Domain.Services;
using ClimateControlSystem.Server.Resources.Domain;
using ClimateControlSystem.Server.Services.Queries;
using MediatR;

namespace ClimateControlSystem.Server.Services.Handlers
{
    public class ProcessMicroclimateHandler : IRequestHandler<ProcessMicroclimateQuery, Prediction>
    {
        private readonly IMonitoringService _predictionService;
        private readonly IMapper _mapper;

        public ProcessMicroclimateHandler(IMonitoringService predictionService, IMapper mapper)
        {
            _predictionService = predictionService;
            _mapper = mapper;
        }

        public async Task<Prediction> Handle(ProcessMicroclimateQuery request, CancellationToken cancellationToken)
        {
            var featuresData = _mapper.Map<FeaturesData>(request.MonitoringRequest);

            return await _predictionService.Predict(featuresData);
        }
    }
}
