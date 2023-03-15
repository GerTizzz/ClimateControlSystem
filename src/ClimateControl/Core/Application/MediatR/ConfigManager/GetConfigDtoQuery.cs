using MediatR;
using Shared.Dtos;

namespace Application.MediatR.ConfigManager
{
    public sealed class GetConfigDtoQuery : IRequest<ConfigsDto>
    {

    }
}
