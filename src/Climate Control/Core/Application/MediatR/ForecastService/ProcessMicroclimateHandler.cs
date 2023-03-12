using AutoMapper;
using Domain.Services;
using MediatR;

namespace Application.MediatR.ForecastService
{
    public class ProcessMicroclimateHandler : IRequestHandler<ProcessMicroclimateQuery, Label>
    {
        private readonly IMonitoringService _predictionService;
        private readonly IMapper _mapper;

        public ProcessMicroclimateHandler(IMonitoringService predictionService, IMapper mapper)
        {
            _predictionService = predictionService;
            _mapper = mapper;
        }

        public async Task<Label> Handle(ProcessMicroclimateQuery request, CancellationToken cancellationToken)
        {
            var featuresData = _mapper.Map<Feature>(request.MonitoringRequest);

            return await _predictionService.Predict(featuresData);
        }
    }
}
