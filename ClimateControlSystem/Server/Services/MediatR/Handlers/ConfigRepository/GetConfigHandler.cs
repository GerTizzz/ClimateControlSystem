using AutoMapper;
using ClimateControlSystem.Server.Domain.Repositories;
using ClimateControlSystem.Server.Resources.Common;
using ClimateControlSystem.Server.Services.MediatR.Queries.ConfigRepository;
using MediatR;

namespace ClimateControlSystem.Server.Services.MediatR.Handlers.ConfigRepository;

public sealed class GetConfigHandler : IRequestHandler<GetConfigQuery, Config>
{
    private readonly IConfigRepository _configRepository;
    private readonly IMapper _mapper;

    public GetConfigHandler(IConfigRepository configRepository, IMapper mapper)
    {
        _configRepository = configRepository;
        _mapper = mapper;
    }
    
    public async Task<Config> Handle(GetConfigQuery request, CancellationToken cancellationToken)
    {
        var configEntity = await _configRepository.GetConfigAsync();

        var config = _mapper.Map<Config>(configEntity);

        return config;
    }
}