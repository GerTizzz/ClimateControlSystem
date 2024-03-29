﻿using AutoMapper;
using ClimateControlSystem.Server.Domain.Repositories;
using ClimateControlSystem.Server.Services.MediatR.Queries.MonitoringsRepository;
using ClimateControlSystem.Shared.Responses;
using MediatR;

namespace ClimateControlSystem.Server.Services.MediatR.Handlers.MonitoringsRepository
{
    public sealed class GetMonitoringsWithAccuracyHandler : IRequestHandler<GetMonitoringsWithAccuracyQuery, List<MonitoringWithAccuracyDto>>
    {
        private readonly IMapper _mapper;
        private readonly IMonitoringsRepository _microclimateRepository;

        public GetMonitoringsWithAccuracyHandler(IMapper mapper, IMonitoringsRepository microclimateRepository)
        {
            _mapper = mapper;
            _microclimateRepository = microclimateRepository;
        }

        public async Task<List<MonitoringWithAccuracyDto>> Handle(GetMonitoringsWithAccuracyQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var monitoringsEntities = await _microclimateRepository.GetMonitoringsWithAccuraciesAsync(request.RequestLimits);

                var monitoringsDTO = monitoringsEntities.Select(entity => _mapper.Map<MonitoringWithAccuracyDto>(entity)).ToList();

                return monitoringsDTO;
            }
            catch (Exception ex)
            {
                return new List<MonitoringWithAccuracyDto>();
            }
        }
    }
}
