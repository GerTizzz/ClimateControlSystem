using ClimateControlSystem.Shared.Common;
using MediatR;

namespace ClimateControlSystem.Server.Services.MediatR.Commands.ConfigManager;

public sealed class UpdateConfigCommand : IRequest<bool>
{
    public ConfigsDto ConfigsDto { get; }

    public UpdateConfigCommand(ConfigsDto configsDto)
    {
        ConfigsDto = configsDto;
    }
}