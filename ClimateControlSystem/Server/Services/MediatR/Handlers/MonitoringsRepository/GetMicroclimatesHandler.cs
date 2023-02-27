using AutoMapper;
using ClimateControlSystem.Server.Domain.Repositories;
using ClimateControlSystem.Server.Services.MediatR.Queries.MonitoringsRepository;
using ClimateControlSystem.Shared.Responses;
using MediatR;

namespace ClimateControlSystem.Server.Services.MediatR.Handlers.MicroclimateRepository
{
    public sealed class GetMicroclimatesHandler : IRequestHandler<GetMicroclimatesQuery, List<MicroclimateDTO>>
    {
        private readonly IMapper _mapper;
        private readonly IMonitoringsRepository _microclimateRepository;

        public GetMicroclimatesHandler(IMapper mapper, IMonitoringsRepository microclimateRepository)
        {
            _mapper = mapper;
            _microclimateRepository = microclimateRepository;
        }

        public async Task<List<MicroclimateDTO>> Handle(GetMicroclimatesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var monitoringsEntities = await _microclimateRepository.GetMicroclimatesAsync(request.RequestLimits);

                var microclimatesDTO = monitoringsEntities.Select(entity => _mapper.Map<MicroclimateDTO>(entity)).ToList();

                return microclimatesDTO;
            }
            catch (Exception ex)
            {
                return new List<MicroclimateDTO>();
            }
        }
    }
}
