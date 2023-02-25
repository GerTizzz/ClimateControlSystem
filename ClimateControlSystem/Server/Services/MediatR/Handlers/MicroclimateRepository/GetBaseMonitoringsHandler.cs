﻿using AutoMapper;
using ClimateControlSystem.Server.Domain.Repositories;
using ClimateControlSystem.Server.Services.MediatR.Queries.MicroclimateRepository;
using ClimateControlSystem.Shared.Responses;
using MediatR;

namespace ClimateControlSystem.Server.Services.MediatR.Handlers.MicroclimateRepository
{
    public sealed class GetBaseMonitoringsHandler : IRequestHandler<GetBaseMonitoringsQuery, List<BaseMonitoringDto>>
    {
        private readonly IMapper _mapper;
        private readonly IMicroclimateRepository _microclimateRepository;

        public GetBaseMonitoringsHandler(IMapper mapper, IMicroclimateRepository microclimateRepository)
        {
            _mapper = mapper;
            _microclimateRepository = microclimateRepository;
        }

        public async Task<List<BaseMonitoringDto>> Handle(GetBaseMonitoringsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var monitoringsEntities = await _microclimateRepository.GetBaseMonitoringsAsync(request.RequestLimits);

                var monitoringsDto = monitoringsEntities.Select(entity => _mapper.Map<BaseMonitoringDto>(entity)).ToList();

                return monitoringsDto;
            }
            catch (Exception ex)
            {
                return new List<BaseMonitoringDto>();
            }
        }
    }
}
