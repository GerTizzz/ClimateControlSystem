using AutoMapper;
using Domain.Services;
using MediatR;

namespace Application.MediatR.ForecastsService;

public class ProcessMicroclimateHandler : IRequestHandler<ProcessMicroclimateQuery, PredictedValue>
{
    private readonly IForecastsService _predictionService;
    private readonly IMapper _mapper;

    public ProcessMicroclimateHandler(IForecastsService predictionService, IMapper mapper)
    {
        _predictionService = predictionService;
        _mapper = mapper;
    }

    public async Task<PredictedValue> Handle(ProcessMicroclimateQuery request, CancellationToken cancellationToken)
    {
        var featuresData = _mapper.Map<Feature>(request.ForecastRequest);

        return await _predictionService.Predict(featuresData);
    }
}