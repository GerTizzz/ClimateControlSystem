using ClimateControl.Shared.Dtos;
using MediatR;

namespace ClimateControl.Server.Services.MediatR.Commands.ConfigManager;

public sealed class UpdateConfigCommand : IRequest<bool>
{
    public ConfigsDto ConfigsDto { get; }

    public UpdateConfigCommand(ConfigsDto configsDto)
    {
        ConfigsDto = configsDto;
    }
}