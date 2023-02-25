using AutoMapper;
using ClimateControlSystem.Server.Domain.Services;
using ClimateControlSystem.Server.Services.MediatR.Queries.ConfigManager;
using ClimateControlSystem.Shared.Common;
using MediatR;

namespace ClimateControlSystem.Server.Services.MediatR.Handlers.ConfigManager;

public sealed class GetConfigDtoHandler : IRequestHandler<GetConfigDtoQuery, ConfigsDto>
{
    private readonly IConfigManager _configManager;
    private readonly IMapper _mapper;

    public GetConfigDtoHandler(IConfigManager configManager, IMapper mapper)
    {
        _configManager = configManager;
        _mapper = mapper;
    }

    public Task<ConfigsDto> Handle(GetConfigDtoQuery request, CancellationToken cancellationToken)
    {
        var config = _configManager.Config;

        var configDto = _mapper.Map<ConfigsDto>(config);

        return Task.FromResult(configDto);
    }
}