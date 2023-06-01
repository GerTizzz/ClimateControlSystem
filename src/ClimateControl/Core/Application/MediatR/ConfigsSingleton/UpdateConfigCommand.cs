using MediatR;
using Shared.Dtos;

namespace Application.MediatR.ConfigsSingleton;

public sealed class UpdateConfigCommand : IRequest<bool>
{
    public ConfigsDto ConfigsDto { get; }

    public UpdateConfigCommand(ConfigsDto configsDto)
    {
        ConfigsDto = configsDto;
    }
}