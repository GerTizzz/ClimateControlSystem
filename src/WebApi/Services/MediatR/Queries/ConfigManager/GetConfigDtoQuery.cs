using MediatR;
using Shared.Dtos;

namespace WebApi.Services.MediatR.Queries.ConfigManager
{
    public sealed class GetConfigDtoQuery : IRequest<ConfigsDto>
    {

    }
}