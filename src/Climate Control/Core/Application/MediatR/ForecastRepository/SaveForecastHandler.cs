using AutoMapper;
using Domain.Repositories;
using MediatR;

namespace Application.MediatR.ForecastRepository
{
    public sealed class SaveForecastHandler : IRequestHandler<SaveForecastCommand, bool>
    {
        private readonly IMonitoringsRepository _predictionRepository;
        private readonly IMapper _mapper;

        public SaveForecastHandler(IMonitoringsRepository predictionRepository, IMapper mapper)
        {
            _predictionRepository = predictionRepository;
            _mapper = mapper;
        }

        public async Task<bool> Handle(SaveForecastCommand request, CancellationToken cancellationToken)
        {
            return await _predictionRepository.SaveForecastAsync(request.Forecast);
        }
    }
}
