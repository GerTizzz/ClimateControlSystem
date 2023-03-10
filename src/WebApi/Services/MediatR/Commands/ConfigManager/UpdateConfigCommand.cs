using MediatR;
using Shared.Dtos;

namespace WebApi.Services.MediatR.Commands.ConfigManager
{
    public sealed class UpdateConfigCommand : IRequest<bool>
    {
        public ConfigsDto ConfigsDto { get; }

        public UpdateConfigCommand(ConfigsDto configsDto)
        {
            ConfigsDto = configsDto;
        }
    }
}