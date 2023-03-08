using AutoMapper;
using ClimateControl.Server.Services.MediatR.Commands.ConfigManager;
using MediatR;

namespace ClimateControl.Server.Services.MediatR.Handlers.ConfigManager;

public sealed class UpdateConfigHandler : IRequestHandler<UpdateConfigCommand, bool>
{
    private readonly IConfigManager _configManager;
    private readonly IMapper _mapper;

    public UpdateConfigHandler(IConfigManager configManager, IMapper mapper)
    {
        _configManager = configManager;
        _mapper = mapper;
    }

    public async Task<bool> Handle(UpdateConfigCommand request, CancellationToken cancellationToken)
    {
        var config = _mapper.Map<Config>(request.ConfigsDto);

        var result = await _configManager.UpdateConfig(config);

        return result;
    }
}